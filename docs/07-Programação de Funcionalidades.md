# Programação de Funcionalidades

<span style="color:red">Pré-requisitos: <a href="2-Especificação do Projeto.md"> Especificação do Projeto</a></span>, <a href="3-Projeto de Interface.md"> Projeto de Interface</a>, <a href="4-Metodologia.md"> Metodologia</a>, <a href="3-Projeto de Interface.md"> Projeto de Interface</a>, <a href="5-Arquitetura da Solução.md"> Arquitetura da Solução</a>

Implementação do sistema descrita por meio dos requisitos funcionais e/ou não funcionais. Deve relacionar os requisitos atendidos com os artefatos criados (código fonte), deverão apresentadas as instruções para acesso e verificação da implementação que deve estar funcional no ambiente de hospedagem.

|ID    | Descrição do Requisito  | Artefato(s) produzido(s) |
|------|-----------------------------------------|----|
|RF-001| O sistema deve ter página de cadastro de usuário. | Login.html / UsuarioController.cs | 
|RF-002| O sistema deve ter página de login.  | Login.html / LoginController.cs |
|RF-003| O sistema deve permitir o usuário cadastrar filmes, séries, jogos e animação. | Home.html - Midia.html / UsuarioController.cs |
|RF-004| O sistema deve permitir ao usuário criar TAGs a serem aplicadas a cada tipo de mídia. | Tag.html/ TagController.cs |
|RF-005| O sistema deve listar as tags por ordem de 'força'. | TagController.cs |
|RF-006| Permissão aos usuários criar tags. | Usuario.html/ UsuarioController.cs |
|RF-007| O sistema deve listar os favoritos. | Usuario.html/ UsuarioController.cs |
|RF-008| Acessar a página e o perfil do usuário. | Usuario.html/ UsuarioController.cs |
|RF-009| O sistema deve permitir exibir o perfil da mídia. | Midia.html/ MidiaContrloler.cs |
|RF-010| O sistema deve permitir o usuário realizar e exibir comentários. | Midia.html/ MidiaController.cs |
|RF-011| O sistema deve permitir listar as mídias para consumir. | Midia.html/ MidiaController.cs |
|RF-012| O sistema deve permitir buscas de mídias. | Home.html - Midia.html/ MidiaController.cs |
|RF-013| O sistema deve permitir o usuário visualizar as mídias top 10. | Home.html - Midia.html/ MidiaController.cs |
|RF-014| O sistema deve exibir as tags do momento. | Midia.html / MidiaController.cs |
