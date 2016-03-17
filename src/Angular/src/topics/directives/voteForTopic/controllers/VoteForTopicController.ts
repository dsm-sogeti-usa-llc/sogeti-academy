import {TopicsService} from '../../../services/TopicsService';
import {Topic} from '../../../models/Topic';
import {Vote} from '../../../models/Vote';

export class VoteForTopicController {
    static $inject = ['$mdDialog', 'TopicsService'];
    
    isSaving: boolean;
    email: string;
    topic: Topic;
    form: angular.IFormController;
    
    get canSave(): boolean {
        return this.form
            && this.form.$valid;
    }
    
    constructor(private $mdDialog: angular.material.IDialogService,
        private topicsService: TopicsService) {
        
    }
    
    cancel(): void {
        this.$mdDialog.hide();
    }
    
    save(): void {
        const vote: Vote = { email: this.email, topicId: this.topic.Id };
        this.isSaving = true;
        this.topicsService.voteForTopic(vote).then(() => {
            this.$mdDialog.hide(true);
            this.isSaving = false;
        });
    }
}