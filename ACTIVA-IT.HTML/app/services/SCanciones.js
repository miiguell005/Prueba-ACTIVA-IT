angular.module('cursos')
    .factory('SCanciones', SCanciones);

function SCanciones($resource, userService) {
    var urlService = userService.urlService;

    return $resource(urlService + "api/Canciones/:id", { id: "@IdCancion" },
        {
            query: {
                method: 'GET', url: urlService + "api/Canciones/:id", isArray: true
            },
            post: {
                method: 'POST', url: urlService + "api/Canciones/:faborito/:inapropiado/:noListar", isArray: false
            }
        });
}
