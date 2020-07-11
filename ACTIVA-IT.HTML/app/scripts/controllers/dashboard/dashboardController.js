
angular.module('cursos')
    .controller('dashboardController', function ($window, userService, SCurso, SPersona) {


        var vm = this;

        vm.rangoEdades;
        vm.ClasificacionGenero;


        vm.init = function () {
            SCurso.getRangoEdades({}, function (dataRangoEdades) {

                vm.rangoEdades = vm.generarArray(dataRangoEdades);
                

            }, function (error) {
                vm.manejarExcepciones(error);
            });

            SCurso.getClasificacionGenero({}, function (dataClasificacionGenero) {

                vm.ClasificacionGenero = vm.generarArray(dataClasificacionGenero);

            }, function (error) {
                vm.manejarExcepciones(error);
            });
        }

        /**
         * Controla el mensaje de error
         * @param {any} error
         */
        vm.manejarExcepciones = function (error) {
            console.log(error)
            if (error && error.data.error)
                toastr.error(error.data.error);

            else
                toastr.error(error.toString());

            console.log("Error", error);
        }


        vm.generarArray = function (objDto) {

            objDto = objDto[0];

            var lista = [];

            var keys = _.keys(objDto);

            for (var i = 0; i < keys.length; i++) {

                var obj = { 'Cantidad': objDto[keys[i]], 'Nombre': keys[i].replace(/_/g, ' ').replace(/C/g, '')};
                lista.push(obj);
            }

            return lista;
        }

        vm.irAtras = function () {
            $window.history.back();
        }



        vm.init();
    });