import {ConfigService} from '../../../core/ConfigService';
import {PresentationDetailService} from './PresentationDetailService';
import {PresentationDetailViewModel} from '../models/PresentationDetailViewModel';

describe('PresentationDetailService', () => {
    let $httpBackend: angular.IHttpBackendService;
    let configService: ConfigService;
    let presentationDetailService: PresentationDetailService;
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$httpBackend_, _ConfigService_, _PresentationDetailService_) => {
        $httpBackend = _$httpBackend_;
        configService = _ConfigService_;
        presentationDetailService = _PresentationDetailService_;
    }));
    
    it('should get presentation detail view model', (done) => {
        const expected: PresentationDetailViewModel = {
            description: 'something',
            files: [],
            id: 'id',
            topic: 'tops'  
        };     
        
        $httpBackend.expectGET(`${configService.apiUrl}/presentations/id`).respond(expected);
        
        presentationDetailService.getDetail('id').then((viewModel) => {
            expect(viewModel).toEqual(expected);
            done();
        });
        $httpBackend.flush();
    });
});