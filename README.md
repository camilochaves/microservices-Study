# microservices-Study
Study of Microservices POC

  ## Inicializando a WEBAPI
### Compilando uma imagem
  - docker build -t webapi:1.0 -f .dockerfile .
  
  ### Testando a API
  - docker run -it --rm -p 8080:80 webapi:1.0
  
  
 Abra o navegador e digite: http://localhost:8080/swagger/index.html  (para acessar todas as APIs)
