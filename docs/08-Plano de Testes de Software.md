# Plano de Testes de Software

<span style="color:red">Pré-requisitos: <a href="2-Especificação do Projeto.md"> Especificação do Projeto</a></span>, <a href="3-Projeto de Interface.md"> Projeto de Interface</a>

Apresente os cenários de testes utilizados na realização dos testes da sua aplicação. Escolha cenários de testes que demonstrem os requisitos sendo satisfeitos.

Não deixe de enumerar os casos de teste de forma sequencial e de garantir que o(s) requisito(s) associado(s) a cada um deles está(ão) correto(s) - de acordo com o que foi definido na seção "2 - Especificação do Projeto". 

Por exemplo:

| **Caso de Teste** 	| **CT-01 – Cadastrar Perfil** 	|
|:---:	|:---:	|
|	Requisito Associado 	| RF 001 - O sistema deve ter página de cadastro de usuário |
| Objetivo do Teste 	| Verificar se o usuário consegue se cadastrar na aplicação. |
| Passos 	| - Acessar o navegador <br> - Informar o endereço do site <br> - Clicar em "Criar conta" <br> - Preencher os campos obrigatórios (e-mail, nome, sobrenome, celular, CPF, senha, confirmação de senha) <br> - Aceitar os termos de uso <br> - Clicar em "Registrar" |
|Critério de Êxito | - O cadastro foi realizado com sucesso. |
|  	|  	|
| **Caso de Teste** 	| **CT-02 – Efetuar Login**	|
|Requisito Associado | RF 002 - O sistema deve ter página de login|
| Objetivo do Teste 	| Verificar se o usuário consegue realizar login. |
| Passos 	| - Acessar o navegador <br> - Informar o endereço do site <br> - Clicar no botão "Entrar" <br> - Preencher o campo de e-mail <br> - Preencher o campo da senha <br> - Clicar em "Login" |
|Critério de Êxito | - O login foi realizado com sucesso. |
|  	|  	|
| **Caso de Teste** 	| **CT-03 – Cadastro de Dados**	|
|Requisito Associado | RF 003 - O sistema deve permitir o usuário cadastrar filmes, séries, jogos e animação|
| Objetivo do Teste 	| Verificar se o usuário consegue inserir dados. |
| Passos 	| - Acessar a tela de cadastro das mídias e inserir as informações solicitadas. <br> 
|Critério de Êxito | - Os dados cadastrados e incluídos com sucesso. |
 |  	|  	|
| **Caso de Teste** 	| **CT-04 – Cadastro das Tags**	|
|Requisito Associado | RF 004 - O sistema deve permitir ao usuário criar TAGs a serem aplicadas a cada tipo de mídia|
| Objetivo do Teste 	| Verificar se o usuário consegue inserir dados das TAGS. |
| Passos 	| - Acessar o cadastro das TAGS e inserir as informações solicitadas   <br> |
|Critério de Êxito | - Os dados cadastrados e incluídos com sucesso. |
 |  	|  	|
| **Caso de Teste** 	| **CT-05 – Listagem das Tags**	|
|Requisito Associado | RF 005 - O sistema deve listar as tags por ordem de 'força'|
| Objetivo do Teste 	| Visualização das TAGS por ordem. |
| Passos 	| - Acessar a página e verificar se as TAGS estão conforme solicitado.   <br> |
|Critério de Êxito | - Visualização das TAGS conforme solicitação. |
 |  	|  	|
| **Caso de Teste** 	| **CT-06 – Permissão aos Usuários Criar Tags**	|
|Requisito Associado | RF 006 - Permissão aos usuários criar tags|
| Objetivo do Teste 	| Verificar se o usuário está conseguindo registrar e adicionar os detalhes da tags. |
| Passos 	| - Acessar o site <br>  - Clicar no link de tags <br> - Inserir os dados conforme a demanda <br> |
|Critério de Êxito | - Sucesso nas inclusões. |
 |  	|  	|
| **Caso de Teste** 	| **CT-07 – Exibição dos Favoritos**	|
|Requisito Associado | RF – 007 O sistema deve listar os favoritos|
| Objetivo do Teste 	| Informar automaticamente ao os favoritos. |
| Passos 	| - Acessar o site <br>  - Acessar a página principal <br> - Acessar o link dos favoritos <br> |
|Critério de Êxito | - Geração no sistema sobre todos favoritos. |
 |  	|  	|
| **Caso de Teste** 	| **CT-08 – O Sistema Deve Permitir Exibir o Perfil do Usuário**	|
|Requisito Associado | RF – 008 Acessar a página e o perfil do usuário|
| Objetivo do Teste 	| Verificar se se o usuário esta com todas as informações corretas. |
| Passos 	| - Acessar o site <br>  - Acessar a página principal <br> - Acessar o link perfil do usuário <br> |
|Critério de Êxito | - Ficar disponível para o usuário toda informação sobre seu perfil. |
|  	|  	|
| **Caso de Teste** 	| **CT-09 – Verificar o Perfil da Mídia Cadastrada**	|
|Requisito Associado | RF – 009 O sistema deve permitir exibir o perfil da mídia|
| Objetivo do Teste 	| Oferecer ao usuário a opção de Verificação da mídia cadastrada. |
| Passos 	| - Acessar o site <br>  - Acessar a página principal <br> - Acessar o link midias <br> |
|Critério de Êxito | - Confirmação das mídias já cadastradas. |
|  	|  	|
| **Caso de Teste** 	| **CT-10 – Teste Cadastro e Exibição dos Comentários**	|
|Requisito Associado |RF 010 – O sistema deve permitir o usuário realizar e exibir comentários|
| Objetivo do Teste 	| Verificar se o sistema está permitindo o cadastro e exibição dos comentários cadastrados. |
| Passos 	| - Acessar o site. <br>  - Acessar a página principal. <br> - Acessar comentários. <br> |
|Critério de Êxito | - Cadastro e exibição dos comentários. | 
|  	|  	|
| **Caso de Teste** 	| **CT-11 – Teste listagem das mídias**	|
|Requisito Associado |RF 011 – O sistema deve permitir listar as mídias para consumir.|
| Objetivo do Teste 	| Verificar se o sistema está exibindo as mídias cadastradas. |
| Passos 	| - Acessar o site. <br>  - Acessar a página principal. <br> - Acessar o link midias. <br> |
|Critério de Êxito | - Visualizar todas as mídias disponíveis.  | 
|  	|  	|
| **Caso de Teste** 	| **CT-12 – Busca de Mídias**	|
|Requisito Associado |RF 012 – O sistema deve permitir buscas de mídias.|
| Objetivo do Teste 	| Verificar se o sistema está permitindo a busca e exibição das mídias. |
| Passos 	| - Acessar o site. <br>  - Acessar a página principal. <br> - Acessar o link midias. <br> |
|Critério de Êxito | - Visualização das mídias solicitadas. | 
|  	|  	|
| **Caso de Teste** 	| **CT 13 – Teste exibição mídias top 10**	|
|Requisito Associado |RF 13 – O sistema deve permitir o usuário visualizar as mídias top 10.|
| Objetivo do Teste 	| Verificar se o sistema está permitindo a exibição das mídias top 10.|
| Passos 	| - Acessar o site. <br>  - Acessar a página principal. <br>  |
|Critério de Êxito | - Visualizar as mídias top 10. | 
|  	|  	|
| **Caso de Teste** 	| **CT 14– Teste exibição tags “do momento”**	|
|Requisito Associado |RF 014 – O sistema deve exibir as tags do momento.|
| Objetivo do Teste 	| Verificar se o sistema está permitindo a exibição das tags do momento.|
| Passos 	| - Acessar o site. <br>  - Acessar a página principal. <br>  |
|Critério de Êxito | - Visualizar as tags do momento.| 
|  	|  	|
||**REQUISITOS NÃO FUNCIONAIS**|
| **Caso de Teste** 	| **CT 15– Verificação Autenticação Usuários**	|
|Requisito Associado |RNF  - 01 O site deve ter senhas de acesso e identificação para os usuários.|
| Objetivo do Teste 	| Verificar se a aplicação e de acesso restrita.|
| Passos 	| - Acessar o navegador <br> - Informar o endereço do site <br> - Informar e-mail  e senha na tela de login <br> - Clicar em login <br> |
|Critério de Êxito | - Acesso de dados somente do usuário autorizado, pela senha.| 
|  	|  	|
| **Caso de Teste** 	| **CT 16– Verificação Autenticação Usuários**	|
|Requisito Associado |RNF  - 02 Os dados pessoais devem ser armazenados sob a Lei Geral de Proteção de Dados Pessoais (LGPD).|
| Objetivo do Teste 	| Verificação da Integridade e proteção dos dados dos usuários cadastrados.|
| Passos 	| Acessar o gerenciador do banco de dados. |
|Critério de Êxito | - Somente a pessoal autorizada tem acesso a todos os dados cadastrados.| 
|  	|  	|
| **Caso de Teste** 	| **CT 17– Compatibilidade de Navegadores**	|
|Requisito Associado |RNF  - 03 O sistema deve funcionar nos principais navegadores, como Chrome, Firefox, Edge, Opera.|
| Objetivo do Teste 	| Verificar se o sistema funciona corretamente nas aplicações disponíveis no mercado.|
| Passos 	| - Acessar o site nos navegadores Chrome, Firefox, Edge e  Opera <br> - Acessar os links <br> - Acessar todas as telas disponíveis <br> |
|Critério de Êxito | - Site funcionando corretamente, sem nenhum erro nos navegadores.| 
|  	|  	|
| **Caso de Teste** 	| **CT 18 Verificação de disponibilidade**	|
|Requisito Associado |RNF  - 04 O site deve estar disponível e acessível para os usuários 24 horas por dia, 7 dias por semana, com um tempo de inatividade planejado mínimo para manutenção.|
| Objetivo do Teste 	| Disponibilidade da aplicação 24 horas por dia 7 dias por semana.|
| Passos 	| - Contratação de um servidor/banco de dados com disponibilidade regional e internacional.|
|Critério de Êxito | - Sistema funcionando sem interrupção.| 
|  	|  	|
| **Caso de Teste** 	| **CT 19 Acessibilidade**	|
|Requisito Associado |RNF  - 05 O site deverá ser fácil de usar, eficiente e acessível.|
| Objetivo do Teste 	| Verificar se o site possui interface intuitiva para pessoas com necessidades especial.|
| Passos 	| - Acessar a aplicação <br> - Acessar o link aumento das letras <br> - Acessar o link pra a leitura da pagina <br> |
|Critério de Êxito | - Aumento das letras e texto gerado em áudio.| 
|  	|  	|
| **Caso de Teste** 	| **CT 20 – Segurança dos dados**	|
|Requisito Associado |RNF  - 06 Os dados do usuário devem ser armazenados e transmitidos de forma segura, utilizando criptografia forte.|
| Objetivo do Teste 	| Verificação de segurança dos dados.|
| Passos 	| - Acessar a aplicação <br> - Verificar se os dados são de fácil acesso <br> |
|Critério de Êxito | - Aplicação com criptografia forte.| 
