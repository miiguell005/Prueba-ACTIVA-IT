angular.module('cursos')
    .factory('SAlbumes', SAlbumes);

function SAlbumes($resource, userService) {
    var urlService = userService.urlService;

    return $resource(urlService + "api/Albumes/:id", { id: "@IdUsuario" },
        {
            query: {
                method: 'GET', url: urlService + "api/Albumes/:pagina", isArray: false
            }
        });
}
