import React from 'react';
import {AppDispatcher} from '../../core/AppDispatcher';
import {createTopic, CREATE_TOPIC_SUCCESS} from '../actions/TopicsActions';

export class CreateTopic extends React.Component {
    constructor(props, context) {
        super(props, context);
        
        this.state = {
            isFormOpen: false,
            name: ''
        };
        this.onNameChange = this.onNameChange.bind(this);
        this.submitForm = this.submitForm.bind(this);
        this.toggleForm = this.toggleForm.bind(this);
        this.cancelForm = this.cancelForm.bind(this);
        
        AppDispatcher.register((action) => {
            switch(action.actionType) {
                case CREATE_TOPIC_SUCCESS:
                    this.cancelForm();
                    break; 
            }
        })
    }
    
    toggleForm(evt) {
        evt.preventDefault();
        
        this.setState({
            isFormOpen: !this.state.isFormOpen,
            name: this.state.name
        });
    }
    
    onNameChange(evt) {
        this.setState({
            isFormOpen: this.state.isFormOpen,
            name: evt.target.value
        });
    }
    
    submitForm() {
        createTopic(this.state.name);
    }
    
    cancelForm() {
        this.setState({
            isFormOpen: false,
            name: ''
        })
    }
    
    render() {
        const formClass = this.state.isFormOpen
            ? 'collapse create-topic-form in'
            : 'collapse create-topic-form'
        return (
            <form className="form-inline" role="form" noValidate>
                <button className="btn btn-primary" onClick={this.toggleForm}>
                    Create Topic
                </button>
                <div className={formClass}>
                    <input className="form-control" type="text" value={this.state.name} onChange={this.onNameChange} required placeholder="Topic" />
                    <button className="btn btn-default" onClick={this.submitForm}>
                        <span className="glyphicon glyphicon-ok"></span>
                    </button>
                    <button className="btn btn-default" onClick={this.cancelForm}>
                        <span className="glyphicon glyphicon-remove"></span>
                    </button>
                </div>
            </form>  
        );
    }
}