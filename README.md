

 - [x] Compilando
 - [x] Projeto organizado
 - [x] Fácil entendimento
	 * <font color="red">Foi um pouco mais difícil de iniciar os testes, pois a base não era criada automaticamente. 
	tive de executar o `Update-Database -Context ApplicationDbContext`

 - [x] Código Limpo
 - [x] Seguindo padrão de escrita em CamelCase
	 -  <font color="blue"> Além de usar bem a nomenclatura, para fins de propriedades em banco de dados, foi utilizado snakecase, ou seja, a propriedade de nome `CodigoAlternativo` no banco de dados passa a ser chamada de `codigo_alternativo`. Parabéns. </color>
 - [x] Baseado em Clean Architeture
	 - <font color="orange">Foi utilizado de certa forma Clean Architeture, mas tem alguns pontos que podemos melhorar, colocando o funcionamento de alguns serviços utilzando validações apenas uma vez. Mas no todo está de parabéns.
 - [x] .Net 8
 - [x] C# 12
 - [x] Documentação
	 -  <font color="purple">Bem documentado, mas em alguns pontos ficou falho, um exemplo é o endpoint <font color="blue">`https://localhost:5000/api/GetAllUser` </font> na verdade ele necessita de um valor, e vem vazio.
	 - <font color="purple"> Este valor em local algum há explicação da necessidade dele, e o pior que ele indo vazio se torna zero, ele é o `Take` da consulta EF que por sua vez por ir Zero, não retornava nada, deu-se a impressão que o endpoint não estava funcionando - <font color="red"><strong>(Este problema ocorre em vários endpoints)</strong></font>. Mas os códigos estão rodando perfeitamente.

# Autenticação
* <font color="orange"> Correta a implmentação de um endpoint separado.       

</font>

# Usuários
### <font color="red">Não verifiquei tudo!
# Produtos
### <font color="red">Não verifiquei tudo!
# Unidades
### <font color="red">Não verifiquei tudo!
# Pessoas


# Validações


 
* <font color="orange"> Todas validações estão bem elaboradas.Porém, ao utilizar MediatR, temos como forçar para que ele mesmo faça as validações, assim não é necessário executá-las novamente no handler. 
* No método Handle de CreatePersonHandler.cs
```csharp
public async Task<CreatePersonResponse> Handle(CreatePersonRequest request, CancellationToken cancellationToken)
{
    #region Validações dos dados informados request e verificações da existência de documento e codígo alternativo no banco de dados

    var person = request.Adapt<Person>();
    var personValidation = await _personValidation.ValidateAsync(person);
    var documentExist = await _context.PropertyDocumentExist(person.Document);
    var alternativeCode = await _context.PropertyAlternativeCodeExist(person.AlternativeCode);

    if (person.Document != null)
        person.Document = person.Document.Replace(".", "").Replace("-", "").Replace("/", "");

    if (!personValidation.IsValid) throw new ValidationException(personValidation.Errors);

    if (alternativeCode != null || documentExist != null)
        throw new CustomException("Já existe uma pessoa com estes dados de documento ou codigo alternativo");

    #endregion Validações dos dados informados request e verificações da existência de documento e codígo alternativo no banco de dados

    person.City = person.City.ToUpper();

    // Gerar identificador unico para cada pessoa criada.
    person.Identifier = _shortIdGeneratorService.GenerateShortId();

    await _context.Create(person);

    await _uow.Commit();

    return person.Adapt<CreatePersonResponse>();
}
```
* <font color="orange">Podemos verificar que, estamos novamente chamando validações que já são processadas pelo MediatR, caso esteja configurado perfeitamente. Existe uma maneira para isto. A ideia aqui, é que quando se chega a este processamento o serviço já tenha o domínio validado, ficando a cargo dele, só as regras de negócio como um todo. Ou seja, aqui neste ponto o request deve ser válido, ou caso não utilize mediator, as validações sejam feitas apenas uma vez, na primeira linha deste método.
</font>
# Utilitários
* <font color="orange">Na classe `ShortIdGeneratorService`, não há necessidade do Replace que é muito mais lento, pode-se usar o comando para geração do Guid assim: 
````c#
var Id = Guid.NewGuid().ToString("N").ToUpper(); 
````
<font color="orange">Desta forma além de gerar o GUID sem os caracteres que foram dados o replace, mantem o padrão de uppercase.

</font>

# Contextos
* <Font color="orange">Não havia necessidade da criação de contextos separados por tipo de banco de dados, a ideia era ter os bancos de dados e tabelas e determinar através apenas da configuração de acesso ao utilizar o AddDbContext, o tipo de banco de dados que iriamos usar, PostgreSql, Sqlite, InMemory. Da forma como foi feito, funciona, mas eu não consigo só por configuração colocar o Identity funcionando em PostgreSql ou InMemory, eu teria de alterar o código. Talvez não me fiz claro no enunciado. Mas funcionou e usando duas bases distintas. 
</font>

# Logs
*<font color="blue"> Log bem elaborado. 
	* <font color="red"><strong>Tomar cuidado que Log do tipo que está com dados sensíveis, sempre tem de ter uma configuração ou melhor ainda, sempre verificar o tipo de ambiente, pois, se for produção só poderá gerar os logs de erro e básicos, pois em produção além de dados sensíveis, teremos um volume enorme de logs. </strong>
</font>

# Avaliação.
 ### <font color="blue">No geral, pelo que vi até então, foi bem elaborado todo desafio, usou melhores técnicas, mas alguns pontos ficaram meio que sobrepostos. Outro ponto que vale destaque , você poderia ter usado para controle de usuários o próprio Identity, não que você tenha feito errado, mas acaba que teve mais trabalho, pois o Microsoft, já nos entregou tudo isto pronto. Mas ficou muito boa sua abordagem.   O serviço `PasswordManager`, ficou bem elaborado, inclusive utilizando método do Identity para verificar a senha. 
 
 # <font color="blue">Parabéns!
> Written with [StackEdit](https://stackedit.io/).
