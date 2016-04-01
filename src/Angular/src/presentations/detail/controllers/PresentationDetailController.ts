import {PresentationDetailService} from '../services/PresentationDetailService';
import {PresentationDetailViewModel} from '../models/PresentationDetailViewModel';
import {FileDetailViewModel} from '../models/FileDetailViewModel';

import '../services/PresentationDetailService';
export class PresentationDetailController {
    static $inject = ['$scope', '$state', 'PresentationDetailService'];
    private _viewModel: PresentationDetailViewModel;
    
    get presentationId(): string {
        return this._viewModel.id;
    }
    
    get topic(): string {
        return this._viewModel.topic;
    }
    
    get description(): string {
        return this._viewModel.description;
    }
    
    get files(): FileDetailViewModel[] {
        return this._viewModel.files;
    }
    
    constructor(private $scope: angular.IScope,
        private $state: angular.ui.IStateService,
        private presentationDetailService: PresentationDetailService) {
        
        const id = $state.params['id'];
        this.presentationDetailService.getDetail(id).then((viewModel) => {
            this.$scope.$applyAsync(() => this._viewModel = viewModel);  
        })
    }
}