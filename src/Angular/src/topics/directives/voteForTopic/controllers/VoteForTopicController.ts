export class VoteForTopicController {
    static $inject = ['$mdDialog'];
    
    constructor(private $mdDialog: angular.material.IDialogService) {
        
    }
    
    cancel(): void {
        this.$mdDialog.hide();
    }
}