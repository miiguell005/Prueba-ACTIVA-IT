
var app = angular.module('cursos', ["ngAnimate", "ngRoute", "ngResource", 'ngSanitize', 'ui.bootstrap' ])

app.config(function ($routeProvider) {

    $routeProvider
        .when('/', {
            templateUrl: 'views/albumes/albums.html',
            controller: 'albumsController',
            controllerAs: '$ctrl'
        })
        .when('/albums', {
            templateUrl: 'views/albumes/albums.html',
            controller: 'albumsController',
            controllerAs: '$ctrl'
        })
        .when('/canciones/:idAlbum', {
            templateUrl: 'views/albumes/canciones.html',
            controller: 'cancionesController',
            controllerAs: '$ctrl'
        })
        .otherwise({
            redirectTo: '/'
        });
})