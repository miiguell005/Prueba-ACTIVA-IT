angular.module('cursos')
    .factory('SCanciones', SCanciones);

function SCanciones($resource, userService) {
    var urlService = userService.urlService;

    return $resource(urlService + "api/Canciones/:id", { id: "@IdCancion" },
        {
            query: {
                method: 'GET', url: urlService + "api/Canciones/:id/:idUsuario", isArray: true
            },
            post: {
                method: 'POST', url: urlService + "api/Canciones/:idUsuario/:faborito/:inapropiado/:noListar", isArray: false
            },
            put: {
                method: 'PUT', url: urlService + "api/Canciones/:idPersona", params: { idPersona: "@IdPersona" }, isArray: false
            },
            remove: {
                method: 'Delete', url: urlService + "api/Canciones/:idPersona", params: { idPersona: "@IdPersona" }, isArray: false
            },
        });
}
