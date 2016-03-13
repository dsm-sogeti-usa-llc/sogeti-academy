import {AppDispatcher} from '../../core/AppDispatcher';
import {TopicsService} from '../services/TopicsService';
import {createLoadingStart, createLoadingEnd} from '../../loading/actions/LoadingActions';

export const LOAD_TOPICS = 'LOAD_TOPICS';
export const LOAD_TOPICS_SUCCESS = 'LOAD_TOPICS_SUCCESS';
export const LOAD_TOPICS_FAILED = 'LOAD_TOPICS_FAILED';

export const CREATE_TOPIC = 'CREATE_TOPIC';
export const CREATE_TOPIC_SUCCESS = 'CREATE_TOPIC_SUCCESS';
export const CREATE_TOPIC_FAILED = 'CREATE_TOPIC_FAILED';

export const VOTE_FOR_TOPIC = 'VOTE_FOR_TOPIC';
export const VOTE_FOR_TOPIC_SUCCESS = 'VOTE_FOR_TOPIC_SUCCESS';
export const VOTE_FOR_TOPIC_FAILED = 'VOTE_FOR_TOPIC_FAILED';

export function createTopic(name) {
    const action = {
        actionType: CREATE_TOPIC,
        data: {
            topicName: name
        }
    };

    createLoadingStart();
    AppDispatcher.dispatch(action);
    TopicsService.createTopic(name)
        .then(
            id => createTopicSuccess(id, name),
            error => createTopicFailed(error)
        );
}

export function createTopicSuccess(id, name) {
    const action = {
        actionType: CREATE_TOPIC_SUCCESS,
        data: {
            Id: id,
            Name: name,
            Votes: 0
        }
    };
    AppDispatcher.dispatch(action);
    createLoadingEnd();
}

export function createTopicFailed(error) {
    const action = {
        actionType: CREATE_TOPIC_FAILED,
        data: {
            error: error
        }
    };
    AppDispatcher.dispatch(action);
    createLoadingEnd();
}

export function voteForTopic(topicId, email) {
    const action = {
        actionType: VOTE_FOR_TOPIC,
        data: {
            topicId: topicId,
            email: email
        }
    };
    createLoadingStart();
    AppDispatcher.dispatch(action);
    TopicsService.voteForTopic(topicId, email)
        .then(
            () => voteForTopicSuccess(topicId),
            error => voteForTopicFailed(error) 
        );
}

export function voteForTopicSuccess(topicId) {
    const action = {
        actionType: VOTE_FOR_TOPIC_SUCCESS,
        data: {
            topicId: topicId
        }
    };
    AppDispatcher.dispatch(action);
    createLoadingEnd();
}

export function voteForTopicFailed(error) {
    const action = {
        actionType: VOTE_FOR_TOPIC_FAILED,
        data: {
            error: error
        }
    };
    AppDispatcher.dispatch(action);
    createLoadingEnd();
}

export function loadTopics() {
    const action = {
        actionType: LOAD_TOPICS,
        data: {}
    };
    
    createLoadingStart();
    AppDispatcher.dispatch(action);

    TopicsService.getTopics().then(
        (topics) => topicsLoadSuccess(topics),
        (error) => topicsLoadFailed(error)
        );
}

export function topicsLoadSuccess(topics) {
    const action = {
        actionType: LOAD_TOPICS_SUCCESS,
        data: {
            topics: topics
        }
    };
    AppDispatcher.dispatch(action);
    createLoadingEnd();
}

export function topicsLoadFailed(error) {
    const action = {
        actionType: LOAD_TOPICS_FAILED,
        data: {
            statusText: error
        }
    };
    AppDispatcher.dispatch(action);
    createLoadingEnd();
}