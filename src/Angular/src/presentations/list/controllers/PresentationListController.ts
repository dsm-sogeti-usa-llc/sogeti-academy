import {Presentation} from '../models/Presentation';
import {PresentationListService} from '../services/PresentationListService';

import '../services/PresentationListService';
export class PresentationListController {
    static $inject = ['$scope', 'PresentationListService'];
    private _presentations: Presentation[];
    
    get presentations(): Presentation[] {
        return this._presentations;
    }
    
    constructor(private $scope: angular.IScope,
        private presentationListService: PresentationListService) {
        
        this.presentationListService.getPresentations().then((pres) => {
            this.$scope.$applyAsync(() => {
                this._presentations = pres;
            })
        })        
    }
}