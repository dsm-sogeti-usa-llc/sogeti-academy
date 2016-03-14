import React from 'react';
import {AppDispatcher} from '../../core/AppDispatcher';
import {voteForTopic, VOTE_FOR_TOPIC_SUCCESS} from '../actions/TopicsActions';

export class TopicItem extends React.Component {
    constructor(props, context) {
        super(props, context);
        
        this.state = {
            isVoteOpen: false,
            email: ''
        };
        
        this.submitVote = this.submitVote.bind(this);
        this.toggleVote = this.toggleVote.bind(this);
        this.cancelVote = this.cancelVote.bind(this);
        this.onEmailChange = this.onEmailChange.bind(this);
        AppDispatcher.register((action) => {
            switch(action.actionType) {
                case VOTE_FOR_TOPIC_SUCCESS:
                    this.cancelVote();
                    break;
            } 
        });
    }
    
    toggleVote(evt) {
        evt.preventDefault();
        
        this.setState({
            isVoteOpen: !this.state.isVoteOpen,
            email: this.state.email
        });
    }
    
    cancelVote() {
        this.setState({
            isVoteOpen: false,
            email: ''
        })
    }
    
    submitVote() {
        voteForTopic(this.props.topic.Id, this.state.email);
    }
    
    onEmailChange(evt) {
        this.setState({
            isVoteOpen: this.state.isVoteOpen,
            email: evt.target.value
        })
    }
    
    render() {
        const formClass = this.state.isVoteOpen
            ? 'collapse in'
            : 'collapse';
        return (
            <li className="list-group-item">
                <form className="form-inline" form="role" noValidate>
                    <button className="btn btn-default btn-vote" onClick={this.toggleVote}>
                        <span className="glyphicon glyphicon-plus"></span>
                    </button>
                    <span>
                        {this.props.topic.Name}&nbsp;
                        <span className="badge">{this.props.topic.Votes}</span>
                    </span>
                    <div className={formClass}>
                        <input className="form-control" type="email" value={this.state.email} onChange={this.onEmailChange} required placeholder="Email" />
                        <button className="btn btn-default" onClick={this.submitVote}>
                            <span className="glyphicon glyphicon-ok"></span>
                        </button>
                        <button className="btn btn-default" onClick={this.cancelVote}>
                            <span className="glyphicon glyphicon-remove"></span>
                        </button>
                    </div>
                </form>
            </li>
        );
    }
}