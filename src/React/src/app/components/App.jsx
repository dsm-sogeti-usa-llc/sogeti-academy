import React from 'react';
import {Link} from 'react-router';

export class App extends React.Component {
    render() {
        return (
            <div>
                <nav className="navbar navbar-default">
                    <div className="container-fluid">
                        <div className="navbar-header">
                            <button type="button"
                                    className="navbar-toggle collapsed"
                                    data-toggle="collapse"
                                    data-target="#navbar-collapse"
                                    aria-expanded="false">
                                <span className="sr-only">Toggle navigation</span>
                                <span className="icon-bar"></span>
                                <span className="icon-bar"></span>
                                <span className="icon-bar"></span>
                            </button>
                            <Link className="navbar-brand" to="/">Sogeti Academy</Link>
                        </div>
                        
                        <div className="collapse navbar-collapse" id="navbar-collapse">
                            <ul className="nav navbar-nav">
                                <li><Link to="/">Home</Link></li>
                                <li><Link to="/topics">Topics</Link></li>
                                <li><Link to="/tooling">Tooling</Link></li>
                            </ul>
                        </div>
                    </div>
                </nav>
                <div className="container">
                    <div className="row">
                        <div className="col-sm-12">
                            {this.props.children}
                        </div>
                    </div>
                </div>
            </div>
        );
    }
} 