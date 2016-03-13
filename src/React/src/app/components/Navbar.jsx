import React from 'react';
import {Link} from 'react-router';

export class Navbar extends React.Component {
    constructor(props, context) {
        super(props, context);
        
        this.state = {
            isNavbarOpen: false
        }
        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.closeNavbar = this.closeNavbar.bind(this);
    }
    
    toggleNavbar() {
        this.setState({
            isNavbarOpen: !this.state.isNavbarOpen
        })
    }
    
    closeNavbar() {
        this.setState({
            isNavbarOpen: false
        });
    }
    
    render() {
        const buttonClass = this.state.isNavbarOpen 
            ? 'navbar-toggle'
            : 'navbar-toggle collapsed';
        const navbarCollaseClass = this.state.isNavbarOpen 
            ? 'collapse navbar-collapse in'
            : 'collapse navbar-collapse';
            
        return (
                <nav className="navbar navbar-default">
                <div className="container-fluid">
                    <div className="navbar-header">
                        <button type="button"
                                className={buttonClass}
                                data-toggle="collapse"
                                data-target="#navbar-collapse"
                                aria-expanded="false"
                                onClick={this.toggleNavbar}>
                            <span className="sr-only">Toggle navigation</span>
                            <span className="icon-bar"></span>
                            <span className="icon-bar"></span>
                            <span className="icon-bar"></span>
                        </button>
                        <Link className="navbar-brand" to="/">Sogeti Academy</Link>
                    </div>
                    
                    <div className={navbarCollaseClass}>
                        <ul className="nav navbar-nav">
                            <li><Link onClick={this.closeNavbar} to="/">Home</Link></li>
                            <li><Link onClick={this.closeNavbar} to="/topics">Topics</Link></li>
                            <li><Link onClick={this.closeNavbar} to="/tooling">Tooling</Link></li>
                        </ul>
                    </div>
                </div>
            </nav>
        );
    }
}