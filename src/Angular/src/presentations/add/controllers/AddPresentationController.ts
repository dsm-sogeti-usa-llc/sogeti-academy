import {AddPresentationService} from '../services/AddPresentationService';
import {AddPresentationViewModel} from '../models/AddPresentationViewModel';

import '../services/AddPresentationService';
export class AddPresentationController {
    static $inject = ['$mdDialog','AddPresentationService'];
    
    topic: string;
    description: string;
    form: angular.IFormController;
    isSaving: boolean;
    
    get canSave(): boolean {
        return this.form 
            && this.form.$valid;
    }
    
    constructor(private $mdDialog: angular.material.IDialogService,
        private addPresentationService: AddPresentationService) {
        
    }
    
    cancel(): void {
        this.$mdDialog.cancel();
    }
    
    save(): void {
        if (!this.form.$valid)
            return;
        
        this.isSaving = true;
        const viewModel: AddPresentationViewModel = {
            topic: this.topic,
            description: this.description
        };
        this.addPresentationService.save(viewModel).then(() => {
            this.$mdDialog.hide();
            this.isSaving = false;
        });
    }
}