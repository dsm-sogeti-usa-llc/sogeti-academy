import {Presentation} from '../models/Presentation';
import {PresentationListController} from './PresentationListController';
import {PresentationListService} from '../services/PresentationListService';
import {AddPresentationViewModel} from '../../add/models/AddPresentationViewModel';
import {createFile} from '../../../createFile';

describe('PresentationListController', () => {
    let $scope: angular.IScope;
    let $rootScope: angular.IRootScopeService;
    let presenationListService: PresentationListService;
    let presentations: Presentation[]; 
    let presentationsPromise: Promise<Presentation[]>
    let createController: () => PresentationListController;
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$controller_, _$rootScope_, _PresentationListService_) => {
        $rootScope = _$rootScope_;
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
    
    it('should add presentation to presentations', () => {
        const viewModel: AddPresentationViewModel = {
            id: 'blah',
            topic: 'blahTopic',
            description: 'not blah',
            files: [
                createFile(),
                createFile(),
                createFile()
            ]
        };
        const controller = createController(); 
        $rootScope.$broadcast('$presentation-added', viewModel);
        
        expect(controller.presentations[0]).toEqual({
            id: viewModel.id,
            topic: viewModel.topic,
            description: viewModel.description,
            filesCount: viewModel.files.length
        })
    });
});