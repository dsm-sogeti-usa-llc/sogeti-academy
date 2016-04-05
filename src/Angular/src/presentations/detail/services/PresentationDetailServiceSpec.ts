import {ConfigService} from '../../../core/ConfigService';
import {PresentationDetailService} from './PresentationDetailService';
import {PresentationDetailViewModel} from '../models/PresentationDetailViewModel'; 
import {FileDetailViewModel} from '../models/FileDetailViewModel'; 

describe('PresentationDetailService', () => {
    let $httpBackend: angular.IHttpBackendService;
    let $window: angular.IWindowService;
    let upload: angular.angularFileUpload.IUploadService;
    let configService: ConfigService;
    let presentationDetailService: PresentationDetailService;
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$httpBackend_, _$window_, _Upload_, _ConfigService_, _PresentationDetailService_) => {
        $window = _$window_;
        $httpBackend = _$httpBackend_;
        upload = _Upload_;
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
    
    it('should save presentation detail', (done) => {
        spyOn(upload, 'upload').and.callThrough();
        
        const viewModel: PresentationDetailViewModel = {
            description: '',
            files: [],
            id: 'something',
            topic: ''  
        };
        $httpBackend.expectPUT(`${configService.apiUrl}/presentations/something`).respond(200);
        
        presentationDetailService.save(viewModel).then(() => {
             $httpBackend.verifyNoOutstandingExpectation();
             expect(upload.upload).toHaveBeenCalled();
             done();
        });
        $httpBackend.flush();
    });
    
    it('should open window to download file', (done) => {
        spyOn($window, 'open').and.callFake(() => {});
                
        const presentation: PresentationDetailViewModel = {
            description: 'asdfasd',
            files: [],
            id: 'big id',
            topic: 'asdf'
        };
        const file: FileDetailViewModel = {
            type: 'txt',
            id: 'my ide',
            name: 'bill',
            size: 32542
        };
        presentationDetailService.downloadFile(file, presentation).then(() => {
            expect($window.open).toHaveBeenCalledWith(
                `${configService.apiUrl}/presentations/${presentation.id}/files/${file.id}`,
                'blank'
            );
            done();
        });
    })
});