import {PresentationDetailService} from '../services/PresentationDetailService';
import {PresentationDetailViewModel} from '../models/PresentationDetailViewModel';
import {FileDetailViewModel} from '../models/FileDetailViewModel';
import {PresentationsState} from '../../PresentationsState';

import '../services/PresentationDetailService';
export class PresentationDetailController {
    static $inject = ['$scope', '$state', 'PresentationDetailService'];
    private _viewModel: PresentationDetailViewModel;
    private _isLoading: boolean;
    private _isSaving: boolean;
    
    form: angular.IFormController;
    get presentationId(): string {
        return this._viewModel.id;
    }
    
    get topic(): string {
        return this._viewModel ? this._viewModel.topic : 'Loading...';
    }
    set topic(value: string) {
        this._viewModel.topic = value;
    }
    
    get description(): string {
        return this._viewModel ? this._viewModel.description : '';
    }
    set description(value: string) {
        this._viewModel.description = value;
    }
    
    get files(): FileDetailViewModel[] {
        return this._viewModel ? this._viewModel.files : [];
    }
    
    get isLoading(): boolean {
        return this._isLoading;
    }
    
    get isSaving(): boolean {
        return this._isSaving;
    }
    
    get canSave(): boolean {
        return this.form 
            && this.form.$valid
            && !this._isLoading
            && !this._isSaving;
    }
    
    constructor(private $scope: angular.IScope,
        private $state: angular.ui.IStateService,
        private presentationDetailService: PresentationDetailService) {
        
        const id = $state.params['id'];
        this._isLoading = true;
        this.presentationDetailService.getDetail(id).then((viewModel) => {
            this.$scope.$applyAsync(() => {
                this._viewModel = viewModel;
                this._isLoading = false;
            });  
        });
    }
    
    save(): void {
        if (!this.canSave)
            return;
        
        this._isSaving = true;
        this.presentationDetailService.save(this._viewModel).then(() => {
            this.$scope.$applyAsync(() => this._isSaving = false);
        })
    }
    
    downloadFile(file: FileDetailViewModel) : void {
        this.presentationDetailService.downloadFile(file, this._viewModel);
    }
    
    goBack(): void {
        this.$state.go(PresentationsState);
    }
}