import {Presentation} from '../models/Presentation';
import {PresentationListService} from '../services/PresentationListService';
import {AddPresentationViewModel} from '../../add/models/AddPresentationViewModel';
import {PresentationDetailState} from '../../detail/PresentationDetailState';

import '../services/PresentationListService';
export class PresentationListController {
    static $inject = ['$scope', '$state', 'PresentationListService'];
    private _presentations: Presentation[];
    
    get presentations(): Presentation[] {
        return this._presentations;
    }
    
    constructor(private $scope: angular.IScope,
        private $state: angular.ui.IStateService,
        private presentationListService: PresentationListService) {
        this._presentations = [];
        this.presentationListService.getPresentations().then((pres) => {
            this.$scope.$applyAsync(() => this._presentations.push(...pres));
        });
        
        this.$scope.$on('$presentation-added', (evt, value) => this.handlePresentationAdded(value));        
    }
    
    goToDetail(presentation: Presentation): void {
        this.$state.go(PresentationDetailState, {
            id: presentation.id
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