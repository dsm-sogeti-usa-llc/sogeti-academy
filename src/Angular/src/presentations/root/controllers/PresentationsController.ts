export class PresentationsController {
    static $inject = ['$rootScope', '$mdDialog'];
    
    constructor(private $rootScope: angular.IRootScopeService,
        private $mdDialog: angular.material.IDialogService) {
        
    }
    
    addPresentation(): void {
        const options: angular.material.IDialogOptions = {
            autoWrap: false,
            clickOutsideToClose: false,
            escapeToClose: false, 
            hasBackdrop: true,
            template: '<add-presentation></add-presentation>'
        };
        this.$mdDialog.show(options)
            .then((viewModel) => this.$rootScope.$broadcast('$presentation-added', viewModel));    
    }
}