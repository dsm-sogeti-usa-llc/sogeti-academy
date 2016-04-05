import {Presentation} from '../models/Presentation';
import {PresentationListService} from '../services/PresentationListService';
import {AddPresentationViewModel} from '../../add/models/AddPresentationViewModel';
import {PresentationDetailState} from '../../detail/PresentationDetailState';

import '../services/PresentationListService';
export class PresentationListController {
    static $inject = ['$scope', '$state', '$mdDialog', 'PresentationListService'];
    private _presentations: Presentation[];
    private _isLoading: boolean;
    
    get presentations(): Presentation[] {
        return this._presentations;
    }
    
    get isLoading(): boolean {
        return this._isLoading;
    }
    
    constructor(private $scope: angular.IScope,
        private $state: angular.ui.IStateService,
        private $mdDialog: angular.material.IDialogService,
        private presentationListService: PresentationListService) {
            
        this._presentations = [];
        this._isLoading = true;
        this.presentationListService.getPresentations().then((pres) => {
            this.$scope.$applyAsync(() => {
                this._presentations.push(...pres);
                this._isLoading = false;
            });
        });   
    }
    
    goToDetail(presentation: Presentation): void {
        this.$state.go(PresentationDetailState, {
            id: presentation.id
        });
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
            .then((viewModel: AddPresentationViewModel) => {
                this._presentations.push({
                    description: viewModel.description,
                    filesCount: viewModel.files.length,
                    id: viewModel.id,
                    topic: viewModel.topic
                })
            });    
    }
    
    private handlePresentationAdded(viewModel: AddPresentationViewModel): void {
        this._presentations.push({
            description: viewModel.description,
            filesCount: viewModel.files.length,
            id: viewModel.id,
            topic: viewModel.topic
        });
    }
}