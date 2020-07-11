angular.module('cursos')

    .service('userService', function ($rootScope) {

        var vm = this;

        //Contiene la tura relativa de la aplicación
        this.urlService = "https://localhost:44365/";
        
    });
