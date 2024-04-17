# Plano de Testes de Software

<span style="color:red">Pré-requisitos: <a href="2-Especificação do Projeto.md"> Especificação do Projeto</a></span>, <a href="3-Projeto de Interface.md"> Projeto de Interface</a>

Apresente os cenários de testes utilizados na realização dos testes da sua aplicação. Escolha cenários de testes que demonstrem os requisitos sendo satisfeitos.

Não deixe de enumerar os casos de teste de forma sequencial e de garantir que o(s) requisito(s) associado(s) a cada um deles está(ão) correto(s) - de acordo com o que foi definido na seção "2 - Especificação do Projeto". 

Por exemplo:
 
| **Caso de Teste** 	| **CT-01 – Cadastrar perfil** 	|
|:---:	|:---:	|
|	Requisito Associado 	| RF 001 - O sistema deve ter página de cadastro de usuário |
| Objetivo do Teste 	| Verificar se o usuário consegue se cadastrar na aplicação. |
| Passos 	| - Acessar o navegador <br> - Informar o endereço do site <br> - Clicar em "Criar conta" <br> - Preencher os campos obrigatórios (e-mail, nome, sobrenome, celular, CPF, senha, confirmação de senha) <br> - Aceitar os termos de uso <br> - Clicar em "Registrar" |
|Critério de Êxito | - O cadastro foi realizado com sucesso. |
|  	|  	|
| **Caso de Teste** 	| **CT-02 – Efetuar login**	|
|Requisito Associado | RF 002 - O sistema deve ter página de login.|
| Objetivo do Teste 	| Verificar se o usuário consegue realizar login. |
| Passos 	| - Acessar o navegador <br> - Informar o endereço do site <br> - Clicar no botão "Entrar" <br> - Preencher o campo de e-mail <br> - Preencher o campo da senha <br> - Clicar em "Login" |
|Critério de Êxito | - O login foi realizado com sucesso. |
|  	|  	|
| **Caso de Teste** 	| **CT-03 – Cadastro de Dados**	|
|Requisito Associado | RF 003 - O sistema deve permitir o usuário cadastrar filmes, séries, jogos e animação.|
| Objetivo do Teste 	| Verificar se o usuário consegue inserir dados. |
| Passos 	| - Acessar a tela de cadastro das mídias e inserir as informações solicitadas. <br> 
|Critério de Êxito | - Os dados cadastrados e incluídos com sucesso. |
 |  	|  	|
| **Caso de Teste** 	| **CT-04 – Cadastro das TAGS**	|
|Requisito Associado | RF 004 - O sistema deve permitir ao usuário criar TAGs a serem aplicadas a cada tipo de mídia.|
| Objetivo do Teste 	| Verificar se o usuário consegue inserir dados das TAGS. |
| Passos 	| - Acessar o cadastro das TAGS e inserir as informações solicitadas.   <br> 
|Critério de Êxito | - Os dados cadastrados e incluídos com sucesso. |
 |  	|  	|
| **Caso de Teste** 	| **CT-05 – Listagem das TAGS**	|
|Requisito Associado | RF 005 - O sistema deve listar as tags por ordem de 'força'.|
| Objetivo do Teste 	| Visualização das TAGS por ordem. |
| Passos 	| - Acessar a página e verificar se as TAGS estão conforme solicitado.   <br> 
|Critério de Êxito | - Visualização das TAGS conforme solicitação. |
 |  	|  	|
| **Caso de Teste** 	| **CT-06 – Permissão aos usuários criar TAGS**	|
|Requisito Associado | RF 006 - Permissão aos usuários criar TAGS.|
| Objetivo do Teste 	| Verificar se o usuário está conseguindo registrar e adicionar os detalhes da TAGS. |
| Passos 	| - Acessar o site <br>  - Clicar no link de TAGS. <br> - Inserir os dados conforme a demanda. <br> |
|Critério de Êxito | - Sucesso nas inclusões. |
 |  	|  	|
| **Caso de Teste** 	| **CT-07 – Exibição dos Favoritos**	|
|Requisito Associado | RF – 007 O sistema deve listar os favoritos.|
| Objetivo do Teste 	| Informar automaticamente ao os favoritos. |
| Passos 	| - Acessar o site. <br>  - Acessar a página principal. <br> - Acessar o link dos favoritos. <br> |
|Critério de Êxito | - Geração no sistema sobre todos favoritos. |
 |  	|  	|
| **Caso de Teste** 	| **CT-08 – O sistema deve permitir exibir o perfil do usuário.**	|
|Requisito Associado | RF – 008 Acessar a página e o perfil do usuário.|
| Objetivo do Teste 	| Verificar se se o usuário esta com todas as informações corretas. |
| Passos 	| - Acessar o site. <br>  - Acessar a página principal. <br> - Acessar o link perfil do usuário. <br> |
|Critério de Êxito | - Ficar disponível para o usuário toda informação sobre seu perfil. |
|  	|  	|
| **Caso de Teste** 	| **CT-09 – Verificar o perfil da mídia cadastrada.**	|
|Requisito Associado | RF – 009 O sistema deve permitir exibir o perfil da mídia.|
| Objetivo do Teste 	| Oferecer ao usuário a opção de Verificação da mídia cadastrada. |
| Passos 	| - Acessar o site. <br>  - Acessar a página principal. <br> - Acessar o link midias. <br> |
|Critério de Êxito | - Confirmação das mídias já cadastradas. |
|  	|  	|
| **Caso de Teste** 	| **CT-10 – Teste cadastro e exibição dos comentários.**	|
|Requisito Associado |RF 010 – O sistema deve permitir o usuário realizar e exibir comentários.|
| Objetivo do Teste 	| Verificar se o sistema está permitindo o cadastro e exibição dos comentários cadastrados. |
| Passos 	| - Acessar o site. <br>  - Acessar a página principal. <br> - Acessar comentários. <br> |
|Critério de Êxito | - Cadastro e exibição dos comentários. | 
|  	|  	|
| **Caso de Teste** 	| **CT-11 – Teste listagem das mídias.**	|
|Requisito Associado |RF 011 – O sistema deve permitir listar as mídias para consumir.|
| Objetivo do Teste 	| Verificar se o sistema está exibindo as mídias cadastradas. |
| Passos 	| - Acessar o site. <br>  - Acessar a página principal. <br> - Acessar o link midias. <br> |
|Critério de Êxito | - Visualizar todas as mídias disponíveis.  | 
|  	|  	|
| **Caso de Teste** 	| **CT-12 – Busca de Mídias.**	|
|Requisito Associado |RF 012 – O sistema deve permitir buscas de mídias.|
| Objetivo do Teste 	| Verificar se o sistema está permitindo a busca e exibição das mídias. |
| Passos 	| - Acessar o site. <br>  - Acessar a página principal. <br> - Acessar o link midias. <br> |
|Critério de Êxito | - Visualização das mídias solicitadas. | 
|  	|  	|
| **Caso de Teste** 	| **CT 13 – Teste exibição mídias top 10.**	|
|Requisito Associado |RF 13 – O sistema deve permitir o usuário visualizar as mídias top 10.|
| Objetivo do Teste 	| Verificar se o sistema está permitindo a exibição das mídias top 10.|
| Passos 	| - Acessar o site. <br>  - Acessar a página principal. <br>  |
|Critério de Êxito | - Visualizar as mídias top 10. | 
|  	|  	|
| **Caso de Teste** 	| **CT 14– Teste exibição tags “do momento”.**	|
|Requisito Associado |RF 014 – O sistema deve exibir as tags do momento.|
| Objetivo do Teste 	| Verificar se o sistema está permitindo a exibição das tags do momento.|
| Passos 	| - Acessar o site. <br>  - Acessar a página principal. <br>  |
|Critério de Êxito | - Visualizar as tags do momento.| 
|  	|  	|
| **Caso de Teste** 	| **CT 14– Teste exibição tags “do momento”.**	|
|Requisito Associado |RF 014 – O sistema deve exibir as tags do momento.|
| Objetivo do Teste 	| Verificar se o sistema está permitindo a exibição das tags do momento.|
| Passos 	| - Acessar o site. <br>  - Acessar a página principal. <br>  |
|Critério de Êxito | - Visualizar as tags do momento.| 


