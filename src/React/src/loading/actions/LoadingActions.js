import {AppDispatcher} from '../../core/AppDispatcher';

export const LOADING_START = 'LOADING_START';
export const LOADING_END = 'LOADING_END';

export function createLoadingStart() {
    dispatch(LOADING_START);
}

export function createLoadingEnd() {
    dispatch(LOADING_END);
}

function dispatch(actionType) {
    AppDispatcher.dispatch({
        actionType: actionType,
        data: {}
    });
}