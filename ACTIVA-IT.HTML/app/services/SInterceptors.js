angular.module('cursos')
  .factory("http", function ($q) {

    //Trabaja la intercepción de la query, cada vez que se hace una petición al servidor se envia
    //El token de autorización en la cabecera 
    return {
      request: function (config) {

        config.headers = config.headers || {};

        if (amplify.store.sessionStorage("user"))
            config.headers.authorization = 'Bearer ' + amplify.store.sessionStorage("user").token;

        return config;
      },
      response: function (response) {
        if (response.status === 401) {
          //asdasdasd
          console.log();
        }

        return response;
      }
    };

  });


angular.module('cursos')
  .config(function ($httpProvider) {

    $httpProvider.interceptors.push("http");

  });
