import {ConfigService} from '../../../core/ConfigService';
import {PresentationListService} from './PresentationListService';

import './PresentationListService';
describe('PresentationListService', () => {
    const apiUrl = 'http://stuff.com';
    let $httpBackend: angular.IHttpBackendService;
    let configService: ConfigService;
    let listService: PresentationListService;
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$httpBackend_, _ConfigService_, _PresentationListService_) => {
        $httpBackend = _$httpBackend_;
        
        configService = _ConfigService_;
        listService = _PresentationListService_;
    }));
    
    it('should get presentations from api', (done) => {
        const listViewModel = {
            presentations: [{}, {}, {}]
        };
        
        $httpBackend.expectGET(`${configService.apiUrl}/presentations`)
            .respond(listViewModel);
            
        listService.getPresentations().then(pres => {
            expect(pres).toEqual(listViewModel.presentations);
            done();
        });
        $httpBackend.flush();
    });
});