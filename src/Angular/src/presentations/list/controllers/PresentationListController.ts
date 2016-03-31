import {Presentation} from '../models/Presentation';
import {PresentationListService} from '../services/PresentationListService';
import {AddPresentationViewModel} from '../../add/models/AddPresentationViewModel';

import '../services/PresentationListService';
export class PresentationListController {
    static $inject = ['$scope', 'PresentationListService'];
    private _presentations: Presentation[];
    
    get presentations(): Presentation[] {
        return this._presentations;
    }
    
    constructor(private $scope: angular.IScope,
        private presentationListService: PresentationListService) {
        this._presentations = [];
        this.presentationListService.getPresentations().then((pres) => {
            this.$scope.$applyAsync(() => this._presentations.push(...pres));
        });
        
        this.$scope.$on('$presentation-added', (evt, value) => this.handlePresentationAdded(value));        
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