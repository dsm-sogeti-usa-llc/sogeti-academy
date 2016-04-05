import {TopicsService} from '../../../services/TopicsService';
import {Topic} from '../../../models/Topic';

export class CreateTopicController {
    static $inject = ['$mdDialog', 'TopicsService'];
    
    isSaving: boolean;
    form: angular.IFormController;
    topic: Topic;
    
    get canSave(): boolean {
        return this.form
            && this.form.$valid;
    }
    
    constructor(private $mdDialog: angular.material.IDialogService,
        private topicsService: TopicsService) {
        this.topic = { name: '', votes: 0 };
    }
    
    cancel(): void {
        this.$mdDialog.cancel();
    }
    
    save(): void {
        if (!this.canSave)
            return;
        
        this.isSaving = true;
        this.topicsService.createTopic(this.topic).then((id) => {
            this.topic.id = id;
            this.$mdDialog.hide(this.topic);
            this.isSaving = false;
        });
    }
}