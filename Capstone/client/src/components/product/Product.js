import React, { useEffect, useState } from "react";
import { NavLink, useNavigate } from "react-router-dom";
import { Button, Card, CardBody, CardImg, CardText, Col, Nav, NavItem, Popover, PopoverHeader, UncontrolledPopover } from "reactstrap";
import { NavLink as RRNavLink } from "react-router-dom";
import { addUserProduct, deleteUserProduct, getAllUserProducts } from "../../modules/productManager";

export const Product = ({ product, userProductId }) => {
    const navigate = useNavigate()
    const [userProduct, setUserProduct] = useState([])
    const [setisUserProduct, setIsUserProduct] = useState(null)

    const getUserProduct = () => {
        getAllUserProducts().then(resp => setUserProduct(resp))

    }


    useEffect(() => {
        getUserProduct();

    }, []);



    return (

        <Card key={product?.id} style={{
            width: '16em'
        }}>
            <div><React.StrictMode><Button id="Popover1" type="button" onClick={(() => addUserProduct(product))} className="btn"><i className="fa-solid fa-person-circle-plus"></i></Button>
                <UncontrolledPopover flip target="Popover1" trigger="click"><PopoverHeader>Added to my Product List</PopoverHeader></UncontrolledPopover></React.StrictMode></div>
            <Col>
                <CardBody onClick={(() => navigate(`/product/${product?.id}`))} >
                    <CardImg top src={product?.imageUrl} alt="Card image cap" />


                    <p>{product?.name}</p>
                    <p>{product?.brand?.name}</p>

                </CardBody>



            </Col>
        </Card >
    )
}