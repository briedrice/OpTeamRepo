// app-league.js
(function () {

    "use strict";
    
    angular.module("app-league")
        .controller("leagueController", leagueController);

    function leagueController($scope) {

        $scope.init = function (vm) {
            $scope.vm = vm;

            $scope.vm.hi = "Hi " + vm.SummonerName;

        }

       
    }
    })();