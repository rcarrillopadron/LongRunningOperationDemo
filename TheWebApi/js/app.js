(function() {
    'use strict';

    angular.module('app', [])
        .factory('api', ['$log', '$http', longRunningService])
        .controller('MainController', ['$log', 'api', mainController]);

    function longRunningService($log, $http) {
        return {
            start: start,
            stop: stop,
            getStatus: getStatus
        };

        function start() {
            return $http.post('api/start');
        }

        function stop() {
            return $http.post('api/stop');
        }

        function getStatus() {
            return $http.get('api/status')
                .then(function(result) {
                    return result.data.lastGeneratedNumber;
                });
        }
    }

    function mainController($log, api) {
        var vm = this;

        vm.start = start;
        vm.stop = stop;
        vm.getLastGeneratedNumber = getLastGeneratedNumber;

        function start() {
            vm.status = 'starting';
            api.start().then(function() {
                vm.status = 'started';
            }, onError);
        }

        function stop() {
            vm.status = 'stopping';
            api.stop().then(function() {
                vm.status = 'stopped';
            }, onError);
        }

        function getLastGeneratedNumber() {
            api.getStatus()
                .then(function (result) {
                    vm.lastGeneratedNumber = result;
                }, onError);
        }

        function onError(error) {
            vm.error = error;
        }
    }
})();