# **DESAFIO .NET/C#**

A prova técnica de desenvolvimento _back-end_ é focada na construção de uma API REST voltada ao contexto de videomonitoramento que atenda aos requisitos descritos abaixo:​


### Requisitos funcionais

-   Criar um novo servidor​;
    
-   Um servidor é composto por ID (guid), nome (string), endereço IP (string), porta IP (integer)​;
    
-   Atualizar um servidor​;
    
-   Remover um servidor​;
    
-   Recuperar um servidor​;
    
-   Checar disponibilidade de um servidor​;
    
-   Verificar se o servidor responde no endereço e porta IP configurados previamente​;
    
-   Listar todos os servidores​;
    
-   Adicionar vídeo à um servidor​;
    
-   Um arquivo de vídeo é composto por ID (guid), descrição (string) e conteúdo binário do vídeo​;
    
-   Remover um vídeo​;
    
-   Recuperar dados cadastrais de um vídeo​;
    
-   Recuperar conteúdo binário de um vídeo;
    
-   ​Listar dados cadastrais de todos os vídeo de um servidor​;
    
-   Reciclar vídeo antigos​;
    
-   Remover os vídeo adicionados a mais de X dias, incluindo o conteúdo binário do vídeo.
#
### Requisitos não-funcionais

-   Todas as operações devem utilizar formato Json (application/json)​
    
    -   Conteúdo binário dos vídeos deve ser enviado/recuperado em formato base64​
        
-   Todas as operações devem utilizar verbos HTTP adequados à operação​
    
-   Todas as operações devem utilizar status HTTP de retorno adequados à operação​
    
-   Persistência das informações pode ser realizada em banco de dados ou sistema de arquivos​
    
    -   Conteúdo binário dos vídeos deve ser mantido no sistema de arquivos​
        
-   Reciclagem de vídeos antigos deve ser realizada em segundo plano (não-bloqueante)​
    
-   Executar a reciclagem em segundo plano e retornar imediatamente o status HTTP 202​

##### **URIs das operações devem ser as seguintes:**
1.  Criar um novo servidor​
    
    -  `/api/server​`
        
2.  Remover um servidor existente​
    
    -`/api/servers/{serverId}​`
        
3.  Recuperar um servidor existente​
    
    - `/api/servers/{serverId}​`
        
4.  Checar disponibilidade de um servidor​
    
    -  `/api/servers/available/{serverId}​`
        
5.  Listar todos os servidores​
    
	   - `/api/servers​`
        
6.  Adicionar um novo vídeo à um servidor​
    
    -  `/api/servers/{serverId}/videos​`
        
7.  Remover um vídeo existente​
    
    -  `/api/servers/{serverId}/videos/{videoId}​`
        
8.  Recuperar dados cadastrais de um vídeo​
    
    -  `/api/servers/{serverId}/videos/{videoId}​`
        
9.  Download do conteúdo binário de um vídeo​
    
    -  `/api/servers/{serverId}/videos/{videoId}/binary​`
        
10.  Listar todos os vídeos de um servidor​
    
	    -  `/api/servers/{serverId}/videos​`
        
11.  Reciclar vídeos antigos​
    
	    -  `/api/recycler/process/{days}​`
        
	    -  `/api/recycler/status​`
 
 ##### **Exemplos de conteúdo Json (application/json):**
 1.  Servidor​
    
	    - `{ "id": "{00A077F6-6ACF-41AF-942A-501BE8FB80F8}", "name":​`
        
	    - `"Servidor 1", "ip": "127.0.0.1", "port": 8080 }​`
        
2.  Vídeo (informações básicas)​
    
	  - `{ "id": "{11A077F6-6ACF-41AF-942A-501BE8FB80B8}", "description":​`
        
    - `"Vídeo 1", "sizeInBytes": 291923 }​`
        
3.  Reciclagem (status de processamento)​
    
    -  `{ "status": "running" }​`
        
    -  `{ "status": "not running" }`
#
### Desejáveis
-   Processamento distribuído
    
-   Comunicação por mensageria
    
-   Testes unitários
#

Não é necessário que você entregue 100% da aplicação, embora seja bastante importante. Vamos levar em consideração a quantidade e a qualidade das funcionalidades desenvolvidas. A ideia principal do desafio é permitir conhecer como você desenvolve, quais seus conhecimentos e experiências, e quais práticas você adota tipicamente para construção de uma aplicação desse tipo.​

​Sinta-se à vontade para usar as ferramentas e bibliotecas que você julgar adequadas e que podem agregar valor à aplicação. E, lembre-se, que iremos focar nossa avaliação principalmente no uso de boas práticas de programação tais como DRY, SOLID, CLEAN e testes automatizados.​
​
Por último, reforçando que seja utilizada a plataforma **.NET/C#** de preferência a versão **.NET 5.0** ou maior para construção da aplicação.
