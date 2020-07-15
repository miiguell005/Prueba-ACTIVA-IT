angular.module('cursos')
    .factory('SUsuario', SUsuario);

function SUsuario($resource, userService) {
    var urlService = userService.urlService;

    return $resource(urlService + "api/Usuarios/:id", { id: "@IdUsuario" },
        {
            query: {
                method: 'GET', url: urlService + "api/Usuarios", isArray: true
            },
            getToken: {
                method: 'GET', url: urlService + "api/Usuarios/Token/:id", isArray: false
            }
        });
}
