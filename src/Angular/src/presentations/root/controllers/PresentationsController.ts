export class PresentationsController {
    static $inject = ['$mdDialog'];
    
    constructor(private $mdDialog: angular.material.IDialogService) {
        
    }
    
    addPresentation(): void {
        const options: angular.material.IDialogOptions = {
            autoWrap: false,
            clickOutsideToClose: false,
            escapeToClose: false, 
            hasBackdrop: true,
            template: '<add-presentation></add-presentation>'
        };
        this.$mdDialog.show(options);    
    }
}