import React, { useState } from 'react';
import { NavLink as RRNavLink } from "react-router-dom";
import {
    Collapse,
    Navbar,
    NavbarToggler,
    NavbarBrand,
    Nav,
    NavItem,
    NavLink,
    UncontrolledDropdown,
    DropdownToggle,
    DropdownMenu,
    DropdownItem
} from 'reactstrap';
import { logout } from '../modules/authManager';

export default function Header({ isLoggedIn, isAdmin, isApproved }) {
    const [isOpen, setIsOpen] = useState(false);
    const toggle = () => setIsOpen(!isOpen);

    return (
        <div>
            <Navbar color="light" light expand="md">
                <NavbarBrand tag={RRNavLink} to="/">BOTB</NavbarBrand>
                <NavbarToggler onClick={toggle} />
                <Collapse isOpen={isOpen} navbar>
                    <Nav className="mr-auto" navbar>
                        { /* When isLoggedIn === true, we will render the Home link */}
                        {isLoggedIn &&
                            <>
                                <NavItem>
                                    <NavLink tag={RRNavLink} to="/">Home</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={RRNavLink} to="/product">Products</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={RRNavLink} to="/ingredient">Ingredients</NavLink>
                                </NavItem>

                                <UncontrolledDropdown nav inNavbar>
                                    <DropdownToggle nav caret>
                                        Options
                                    </DropdownToggle>
                                    <DropdownMenu end>
                                        <DropdownItem><NavLink tag={RRNavLink} to="/myProducts">My Products</NavLink></DropdownItem>
                                        <DropdownItem divider />
                                        <DropdownItem><NavLink tag={RRNavLink} to="/hazard">Hazards</NavLink></DropdownItem>
                                        <DropdownItem><NavLink tag={RRNavLink} to="/type">Types</NavLink></DropdownItem>
                                        <DropdownItem><NavLink tag={RRNavLink} to="/brand">Brands</NavLink></DropdownItem>


                                    </DropdownMenu>
                                </UncontrolledDropdown>
                            </>
                        }
                        {isAdmin &&
                            <>
                                <NavItem>

                                </NavItem>
                            </>

                        }
                    </Nav>
                    <Nav navbar>
                        {isLoggedIn &&
                            <>
                                <NavItem>
                                    <a aria-current="page" className="nav-link"
                                        style={{ cursor: "pointer" }} onClick={logout}>Logout</a>
                                </NavItem>


                            </>
                        }
                        {!isLoggedIn &&
                            <>
                                <NavItem>
                                    <NavLink tag={RRNavLink} to="/login">Login</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={RRNavLink} to="/register">Register</NavLink>
                                </NavItem>

                            </>
                        }
                    </Nav>
                </Collapse>
            </Navbar>
        </div>
    );
}
