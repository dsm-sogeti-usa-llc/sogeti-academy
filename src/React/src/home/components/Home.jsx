import React from 'react';
import {Link} from 'react-router';

export class Home extends React.Component {
    render() {
        return (
            <div className="jumbotron">
                <h1>Welcome to Sogeti Academy!</h1>
                <p>
                    Please feel free to vote on the topics to be explored in the academy.
                </p>
                <p>
                    <Link className="btn btn-primary" to="/topics">Topics</Link>
                </p>
            </div>
        )
    }
}