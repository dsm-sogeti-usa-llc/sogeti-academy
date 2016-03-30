import {Presentation} from '../models/Presentation';
import {PresentationListController} from './PresentationListController';
import {PresentationListService} from '../services/PresentationListService';

describe('PresentationListController', () => {
    let $scope: angular.IScope;
    let presenationListService: PresentationListService;
    let presentations: Presentation[]; 
    let presentationsPromise: Promise<Presentation[]>
    let createController: () => PresentationListController;
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$controller_, _$rootScope_, _PresentationListService_) => {
        $scope = _$rootScope_.$new();
        
        presentations = [];
        presenationListService = _PresentationListService_;
        spyOn(presenationListService, 'getPresentations').and.callFake(() => {
            presentationsPromise = Promise.resolve(presentations);
            return presentationsPromise;
        });
        
        createController = () => {
            return _$controller_(PresentationListController, {
                $scope: $scope,
                PresentationListService: presenationListService
            })
        };
    }));
    
    it('should get presentations from service', (done) => {
        spyOn($scope, '$applyAsync').and.callFake(cb => cb());

        presentations.push({ id: '', topic: '', description: '', filesCount: 0 });
        presentations.push({ id: '', topic: '', description: '', filesCount: 0 });
        presentations.push({ id: '', topic: '', description: '', filesCount: 0 });
        
        const controller = createController();
        presentationsPromise.then(() => {
            expect(controller.presentations).toEqual(presentations);
            done();
        });
    });
});