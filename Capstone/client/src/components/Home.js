import React, { useEffect, useState } from "react"
import { ProductList } from "./product/ProductList"
import { ProductSearch } from "./product/ProductSearch"
import './Home.css'
import { Button, Card, CardBody, Collapse, Nav, NavItem, NavLink, Offcanvas, OffcanvasBody, OffcanvasHeader, UncontrolledCollapse } from "reactstrap"
import { getAllTypes } from "../modules/typeManager"
import { getAllBrands } from "../modules/brandManager"
import { NavLink as RRNavLink, useNavigate } from "react-router-dom";

export const Home = ({ isAdmin, isApproved }) => {
    const navigate = useNavigate()
    const [isOpen, setOpen] = useState(false)
    const [types, setTypes] = useState([])
    const [brands, setBrands] = useState([])
    const toggle = () => setOpen(!isOpen)

    const getTypes = () => {
        getAllTypes().then(t => setTypes(t))
    }
    const getBrands = () => {
        getAllBrands().then(b => setBrands(b))
    }


    useEffect(() => {
        getTypes();
        getBrands();
    }, []);


    return <>
        <section className="websitetitle">
            <span >back of the bottle</span>
        </section>
        <section className="product-view-select">
            <div className="view-products">
                <button onClick={(() => navigate(`/product/`))} className="btn"><h3>View all Products</h3></button>
            </div>
            <div className="center"><span></span></div>
            <div className="view-ingredients">
                <button onClick={(() => navigate(`/ingredient/`))} className="btn"><h3>View all Ingredients</h3></button>
            </div>
        </section >


        <section className="typesa">
            <h3>View Products by Type</h3>
            <div className="typeboxs">
                {
                    types.map((t) => {
                        return <p key={t.id} className="typewrappera"><button className="btn" onClick={(() => navigate(`/product/type/${t.id}`))}>{t.name}</button></p>
                    })
                }
            </div>
        </section>

        <section className="typesa">
            <h3>View Products by Brand </h3>
            <div className="typeboxs">
                {
                    brands.map((b) => {
                        return <p key={b.id} className="typewrappera"><button key={b.id} className="btn" onClick={(() => navigate(`/product/brand/${b.id}`))}>{b.name}</button></p>
                    })
                }
            </div>
        </section>

    </>
}