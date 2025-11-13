# Guia de Boas Práticas para Models e Controllers com Entity Framework e ASP.NET Core

Este documento resume as estratégias e padrões adotados para modelagem de dados com Entity Framework e a implementação de controllers na API, visando resolver problemas comuns como referências cíclicas, complexidade em requisições `POST`/`PUT` e validação de dados em entidades relacionadas.

## 1. Modelagem de Entidades (Models)

A forma como as classes de modelo são estruturadas é fundamental para o correto funcionamento do Entity Framework e para a serialização/desserialização de JSON.

### Relacionamento 1:N (Um-para-Muitos)

Tomando como exemplo `Motorista` (lado "1") e `Veiculo` (lado "N"), onde um motorista pode ter vários veículos, mas um veículo pertence a apenas um motorista.

**Práticas aplicadas:**

1.  **Coleção Nula (`?`) no Lado "1":**
    Na classe `Motorista`, a coleção de veículos foi declarada como anulável (`ICollection<Veiculo>?`).

    ```csharp
    // Em Models/Motorista.cs
    public class Motorista
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        // ... outras propriedades

        // A coleção é anulável para que não seja exigida ao criar/atualizar um motorista.
        public ICollection<Veiculo>? Veiculos { get; set; }
    }
    ```

    *   **Por quê?** Isso evita que o framework exija a inclusão da lista de veículos ao criar um novo motorista. O foco é cadastrar o motorista, e os veículos podem ser associados depois.

2.  **Uso de `[JsonIgnore]` no Lado "N":**
    Na classe `Veiculo`, a propriedade de navegação de volta para `Motorista` foi decorada com `[JsonIgnore]`.

    ```csharp
    // Em Models/Veiculo.cs
    public class Veiculo
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        // ... outras propriedades

        // Chave estrangeira explícita
        public int MotoristaId { get; set; }

        // JsonIgnore evita a serialização do objeto Motorista, que por sua vez
        // conteria a lista de Veiculos, causando uma referência cíclica.
        [JsonIgnore]
        public Motorista? Motorista { get; set; }
    }
    ```

    *   **Por quê?** Ao serializar um `Veiculo` para JSON, a propriedade `Motorista` não será incluída. Isso quebra a **referência cíclica** (`Veiculo` -> `Motorista` -> `Veiculos`), que causava o loop infinito e o erro na API.

3.  **Chave Estrangeira Explícita:**
    A propriedade `MotoristaId` foi mantida no modelo `Veiculo`. Isso torna as associações mais simples e diretas, tanto no código quanto nas requisições da API.

### Relacionamento N:N (Muitos-para-Muitos)

O relacionamento entre `Veiculo` e `Vaga` é intermediado pela entidade `RegistroEstacionamento`. Um veículo pode estacionar em várias vagas (em momentos diferentes) e uma vaga pode ser ocupada por vários veículos (em momentos diferentes).

**Práticas aplicadas:**

A entidade `RegistroEstacionamento` funciona como uma tabela de junção que, além das chaves estrangeiras, contém dados próprios (como `DataEntrada` e `DataSaida`).

```csharp
// Em Models/RegistroEstacionamento.cs
public class RegistroEstacionamento
{
    public int Id { get; set; }
    public DateTime DataEntrada { get; set; }
    // ...

    // Chaves estrangeiras explícitas
    public int VeiculoId { get; set; }
    public int VagaId { get; set; }

    // Propriedades de navegação ignoradas para evitar referências cíclicas
    [JsonIgnore]
    public Veiculo? Veiculo { get; set; }
    [JsonIgnore]
    public Vaga? Vaga { get; set; }
}
```

*   **Por quê?** Assim como no caso 1:N, o `[JsonIgnore]` nas propriedades de navegação (`Veiculo` e `Vaga`) impede que a serialização JSON tente incluir os objetos completos, o que geraria novas referências cíclicas e payloads desnecessariamente grandes. Para criar ou atualizar um `RegistroEstacionamento`, basta fornecer `VeiculoId` e `VagaId`.

## 2. Estratégias nas Controllers

As controllers foram ajustadas para simplificar as operações de `POST` e `PUT`, focando no uso de IDs para representar relacionamentos.

### `POST` (Criação)

Ao criar uma entidade que pertence a outra (ex: `Veiculo`), a requisição não precisa mais enviar o objeto pai completo.

**Antes:** A API esperava um objeto `Motorista` aninhado dentro do `Veiculo`.
**Agora:** A API espera apenas o `MotoristaId`.

**Exemplo de payload para criar um `Veiculo`:**

```json
{
  "placa": "ABC-1234",
  "modelo": "Fusca",
  "cor": "Azul",
  "motoristaId": 1
}
```

O `VeiculosController` lida com isso da seguinte forma:

```csharp
// Em Controllers/VeiculosController.cs
[HttpPost]
public async Task<ActionResult<Veiculo>> PostVeiculo(Veiculo veiculo)
{
    // O Entity Framework automaticamente associa o Veiculo ao Motorista
    // correto usando o valor de veiculo.MotoristaId.
    _context.Veiculos.Add(veiculo);
    await _context.SaveChangesAsync();

    return CreatedAtAction("GetVeiculo", new { id = veiculo.Id }, veiculo);
}
```

*   **Vantagem:** A requisição fica mais limpa, leve e intuitiva. A validação do `Motorista` não interfere mais no cadastro do `Veiculo`.

### `PUT` (Atualização)

A mesma lógica do `POST` se aplica ao `PUT`. Para alterar o proprietário de um veículo, por exemplo, basta enviar o novo `MotoristaId` no corpo da requisição.

O método `PUT` do controller já está preparado para lidar com a atualização de propriedades simples e chaves estrangeiras. O Entity Framework detecta a mudança no `MotoristaId` e atualiza o relacionamento no banco de dados.

```csharp
// Em Controllers/VeiculosController.cs
[HttpPut("{id}")]
public async Task<IActionResult> PutVeiculo(int id, Veiculo veiculo)
{
    if (id != veiculo.Id)
    {
        return BadRequest();
    }

    // Marca a entidade como modificada. O EF se encarrega de atualizar
    // os campos, incluindo o MotoristaId se ele tiver sido alterado.
    _context.Entry(veiculo).State = EntityState.Modified;

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        // ...
    }

    return NoContent();
}
```

## Conclusão

A combinação de **coleções anuláveis**, **`[JsonIgnore]`** em propriedades de navegação e o **uso de chaves estrangeiras explícitas** nas requisições `POST`/`PUT` cria uma API mais robusta, eficiente e fácil de consumir. Essa abordagem resolve os problemas de referência cíclica e simplifica a criação e atualização de entidades relacionadas.
