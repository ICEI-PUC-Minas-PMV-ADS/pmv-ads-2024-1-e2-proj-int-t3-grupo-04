# Arquitetura da Solução

<span style="color:red">Pré-requisitos: <a href="3-Projeto de Interface.md"> Projeto de Interface</a></span>

Definição de como o software é estruturado em termos dos componentes que fazem parte da solução e do ambiente de hospedagem da aplicação.

## Modelo ER (Projeto Conceitual)

O Modelo ER representa através de um diagrama como as entidades (coisas, objetos) se relacionam entre si na aplicação interativa.

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2024-1-e2-proj-int-t3-grupo-04/assets/72924198/30edcef5-8b3e-4971-976d-792b2cb60256)


## Modelo de Dados & Diagrama de Classes

O projeto da base de dados corresponde à representação das entidades e relacionamentos identificadas no Modelo ER, no formato de tabelas, com colunas e chaves primárias/estrangeiras necessárias para representar corretamente as restrições de integridade.
 
![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2024-1-e2-proj-int-t3-grupo-04/assets/72924198/f0e6eaa9-6e74-4762-ae44-42b087b4e8a1)


## Tecnologias Utilizadas

Para o desenvolvimento do front-end será utilizada a combinação entre HTML, CSS e Javascript, sem uso de um framework específico a princípio.

Para persistência dos dados que forem necessários, dados de login e sessão de usuário, será utilizado o sistema de Localstorage do navegador, onde dados podem ser armazenados em formato de texto simples ou JSON.

Para o desenvolvimento do back-end utilizaremos os frameworks .Net Core e o Asp Net Core. Ambos são frameworks cross-platform (MAC, Linux, Windows) utilizados para desenvolvimento de aplicações web e aplicações desktop. O Asp Net Core será utilizado para as tratativas da API e da web, enquanto o .NET e suas funcionalidades seram utilizados para realizar a comunicação com a database, manipulação de dados e gerenciamento de processos internos.
