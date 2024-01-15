

 - [x] Compilando
 - [x] Projeto organizado
 - [x] F�cil entendimento
	 * <font color="red">Foi um pouco mais dif�cil de iniciar os testes, pois a base n�o era criada automaticamente. 
	tive de executar o `Update-Database -Context ApplicationDbContext`

 - [x] C�digo Limpo
 - [x] Seguindo padr�o de escrita em CamelCase
	 -  <font color="blue"> Al�m de usar bem a nomenclatura, para fins de propriedades em banco de dados, foi utilizado snakecase, ou seja, a propriedade de nome `CodigoAlternativo` no banco de dados passa a ser chamada de `codigo_alternativo`. Parab�ns. </color>
 - [x] Baseado em Clean Architeture
	 - <font color="orange">Foi utilizado de certa forma Clean Architeture, mas tem alguns pontos que podemos melhorar, colocando o funcionamento de alguns servi�os utilzando valida��es apenas uma vez. Mas no todo est� de parab�ns.
 - [x] .Net 8
 - [x] C# 12
 - [x] Documenta��o
	 -  <font color="purple">Bem documentado, mas em alguns pontos ficou falho, um exemplo � o endpoint <font color="blue">`https://localhost:5000/api/GetAllUser` </font> na verdade ele necessita de um valor, e vem vazio.
	 - <font color="purple"> Este valor em local algum h� explica��o da necessidade dele, e o pior que ele indo vazio se torna zero, ele � o `Take` da consulta EF que por sua vez por ir Zero, n�o retornava nada, deu-se a impress�o que o endpoint n�o estava funcionando - <font color="red"><strong>(Este problema ocorre em v�rios endpoints)</strong></font>. Mas os c�digos est�o rodando perfeitamente.

# Autentica��o
* <font color="orange"> Correta a implmenta��o de um endpoint separado.       

</font>

# Usu�rios
### <font color="red">N�o verifiquei tudo!
# Produtos
### <font color="red">N�o verifiquei tudo!
# Unidades
### <font color="red">N�o verifiquei tudo!
# Pessoas
* <font color="orange">`ShortId`em pessoas, n�o est� em uppercase.
* <font color="orange">C�digo alteranativo n�o est� sendo salvo, ou melhor, est� sendo salvo como `vazio`.
* <font color="orange">H� um erro na valida��o para altera��o de pessoas.
* <font color="red"/>Creio que linha de valida��o, <font color="purple">`RuleFor(x => x.AlternativeIdentifier).MustAsync(AlternativeCodeDoesNotExist).WithMessage("The alternative Identifier given it's already on database.");`</font> est� com a l�gica errada, pois a finalidade seria evitar que n�o se cadastre mais de um <font color="purple">`AlternativeIdentifier`</font> iguais, mas, da forma que est� n�o deixar� fazer nenhuma altera��o, pois ap�s inclus�o sempre haver� um registro igual.
* A mensagem de exception em <font color="purple">`UpdatePersonHandler`</font>, quando n�o consegue recuperar a pessoa, ela � muito gen�rica e n�o d� para poder saber o que deve ser feito. Ao tentar recuperar a pessoa para atualiza��o. Se para atualizar eu sou obrigado a informar o <font color="purple">`ShortId`</font> ele deve ser validado e n�o ser vazio. 
* Outro ponto, que o <font color="purple">`ShortId`</font> para atualiza��o ou vai no corpo da requisi��o no como uma vari�vel de post, nos dois n�o pode.
</font>

# Valida��es


 
* <font color="orange"> Todas valida��es est�o bem elaboradas.Por�m, ao utilizar MediatR, temos como for�ar para que ele mesmo fa�a as valida��es, assim n�o � necess�rio execut�-las novamente no handler. 
* No m�todo Handle de CreatePersonHandler.cs
```csharp
public async Task<CreatePersonResponse> Handle(CreatePersonRequest request, CancellationToken cancellationToken)
{
    #region Valida��es dos dados informados request e verifica��es da exist�ncia de documento e cod�go alternativo no banco de dados

    var person = request.Adapt<Person>();
    var personValidation = await _personValidation.ValidateAsync(person);
    var documentExist = await _context.PropertyDocumentExist(person.Document);
    var alternativeCode = await _context.PropertyAlternativeCodeExist(person.AlternativeCode);

    if (person.Document != null)
        person.Document = person.Document.Replace(".", "").Replace("-", "").Replace("/", "");

    if (!personValidation.IsValid) throw new ValidationException(personValidation.Errors);

    if (alternativeCode != null || documentExist != null)
        throw new CustomException("J� existe uma pessoa com estes dados de documento ou codigo alternativo");

    #endregion Valida��es dos dados informados request e verifica��es da exist�ncia de documento e cod�go alternativo no banco de dados

    person.City = person.City.ToUpper();

    // Gerar identificador unico para cada pessoa criada.
    person.Identifier = _shortIdGeneratorService.GenerateShortId();

    await _context.Create(person);

    await _uow.Commit();

    return person.Adapt<CreatePersonResponse>();
}
```
* <font color="orange">Podemos verificar que, estamos novamente chamando valida��es que j� s�o processadas pelo MediatR, caso esteja configurado perfeitamente. Existe uma maneira para isto. A ideia aqui, � que quando se chega a este processamento o servi�o j� tenha o dom�nio validado, ficando a cargo dele, s� as regras de neg�cio como um todo. Ou seja, aqui neste ponto o request deve ser v�lido, ou caso n�o utilize mediator, as valida��es sejam feitas apenas uma vez, na primeira linha deste m�todo.
</font>
# Utilit�rios
* <font color="orange">Na classe `ShortIdGeneratorService`, n�o h� necessidade do Replace que � muito mais lento, pode-se usar o comando para gera��o do Guid assim: 
````c#
var Id = Guid.NewGuid().ToString("N").ToUpper(); 
````
<font color="orange">Desta forma al�m de gerar o GUID sem os caracteres que foram dados o replace, mantem o padr�o de uppercase.

</font>

# Contextos
* <Font color="orange">N�o havia necessidade da cria��o de contextos separados por tipo de banco de dados, a ideia era ter os bancos de dados e tabelas e determinar atrav�s apenas da configura��o de acesso ao utilizar o AddDbContext, o tipo de banco de dados que iriamos usar, PostgreSql, Sqlite, InMemory. Da forma como foi feito, funciona, mas eu n�o consigo s� por configura��o colocar o Identity funcionando em PostgreSql ou InMemory, eu teria de alterar o c�digo. Talvez n�o me fiz claro no enunciado. Mas funcionou e usando duas bases distintas. 
</font>

# Logs
*<font color="blue"> Log bem elaborado. 
	* <font color="red"><strong>Tomar cuidado que Log do tipo que est� com dados sens�veis, sempre tem de ter uma configura��o ou melhor ainda, sempre verificar o tipo de ambiente, pois, se for produ��o s� poder� gerar os logs de erro e b�sicos, pois em produ��o al�m de dados sens�veis, teremos um volume enorme de logs. </strong>
</font>

# Avalia��o.
 ### <font color="blue">No geral, pelo que vi at� ent�o, foi bem elaborado todo desafio, usou melhores t�cnicas, mas alguns pontos ficaram meio que sobrepostos. Outro ponto que vale destaque , voc� poderia ter usado para controle de usu�rios o pr�prio Identity, n�o que voc� tenha feito errado, mas acaba que teve mais trabalho, pois o Microsoft, j� nos entregou tudo isto pronto. Mas ficou muito boa sua abordagem.   O servi�o `PasswordManager`, ficou bem elaborado, inclusive utilizando m�todo do Identity para verificar a senha. 
 
 # <font color="blue">Parab�ns!
> Written with [StackEdit](https://stackedit.io/).