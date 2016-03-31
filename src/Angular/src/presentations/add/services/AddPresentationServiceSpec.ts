import {AddPresentationService} from './AddPresentationService';
import {AddPresentationViewModel} from '../models/AddPresentationViewModel';
import {ConfigService} from '../../../core/ConfigService';

describe('AddPresentationService', () => {
    let $httpBackend: angular.IHttpBackendService;
    let upload: angular.angularFileUpload.IUploadService;
    let configService: ConfigService;
    let addPresentationService: AddPresentationService;
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$httpBackend_, _Upload_, _ConfigService_, _AddPresentationService_) => {
        upload = _Upload_;
        $httpBackend = _$httpBackend_;
        configService = _ConfigService_;
        addPresentationService = _AddPresentationService_;
    }));
    
    it('should upload view model to api', (done) => {
        const expectedId = 'asdfasdf';
        const viewModel: AddPresentationViewModel = {
            topic: 'tdd',
            description: 'stuff'  
        };
        spyOn(upload, 'upload').and.callThrough();
        
        const url = `${configService.apiUrl}/presentations`;
        $httpBackend.whenPOST(url).respond(expectedId);
            
        addPresentationService.save(viewModel).then((id) => {
            expect(id).toBe(expectedId);
            expect(upload.upload).toHaveBeenCalledWith({
                data: viewModel,
                url: url,
                method: 'POST'
            });
            done();
        });
        $httpBackend.flush();
    })
});