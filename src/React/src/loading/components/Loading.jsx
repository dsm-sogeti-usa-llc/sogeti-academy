import * as React from 'react';
import {LoadingStore} from '../stores/LoadingStore';
import jQuery from 'jquery';

const store = new LoadingStore();
export class Loading extends React.Component {
    constructor(props, context) {
        super(props, context);
        
        this.state = store.getState();
        store.addChangeListener(this.onChange.bind(this));  
    }
    
    onChange() {
        this.setState(store.getState());
        if (this.state.isLoading) {
           jQuery('#loadingModal').modal({
               show: true,
               keyboard: false,
               backdrop: 'static'
           })
        } else {
            jQuery('#loadingModal').modal({
                show: false
            });
        }
    }
        
    render() {
        const progressbarStyle = { width: '100%' };
        return (
            <div id="loadingModal" className="modal fade" role="dialog">
                <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h4>Loading...</h4>
                        </div>
                        <div className="modal-body">
                            <div className="progress progress-striped active">
                                <div className="progress-bar" role="progressbar" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style={progressbarStyle}>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}