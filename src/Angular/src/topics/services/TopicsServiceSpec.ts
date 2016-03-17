import {TopicsService} from './TopicsService';
import {ConfigService} from '../../core/ConfigService';
import {Topic} from '../models/Topic';

describe('TopicsService', () => {
    let $httpBackend: angular.IHttpBackendService;
    let topicsService: TopicsService;
    let configService: ConfigService;

    beforeEach(angular.mock.module('sogeti-academy'));

    beforeEach(angular.mock.inject((_$httpBackend_, _ConfigService_, _TopicsService_) => {
        $httpBackend = _$httpBackend_;
        topicsService = _TopicsService_;
        configService = _ConfigService_;
    }));

    it('should get topics from api', (done) => {
        const url = `${configService.apiUrl}/topics`;
        const topics: Topic[] = [
            { Name: 'one', Votes: 1 },
            { Name: 'two', Votes: 0 },
            { Name: 'three', Votes: -1 }
        ];
        $httpBackend.expectGET(url).respond({
            Topics: topics
        });
        
        topicsService.getAll().then((actual: Topic[]) => {
            expect(actual).toEqual(topics);
            done();
        });
        $httpBackend.flush();
    });

    afterEach(() => {
        $httpBackend.verifyNoOutstandingExpectation();
        $httpBackend.verifyNoOutstandingRequest();
    });
});