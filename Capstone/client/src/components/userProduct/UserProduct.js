import React from "react";
import { NavLink, useNavigate } from "react-router-dom";
import { Button, Card, CardBody, CardImg, CardText, Col } from "reactstrap";
import { NavLink as RRNavLink } from "react-router-dom";
import { addUserProduct, deleteUserProduct } from "../../modules/productManager";
import './UserProduct.css'

export const UserProduct = ({ product, userProduct, userProductId, getUserProducts }) => {
    const navigate = useNavigate()

    const deleteButton = () => {
        return deleteUserProduct(userProductId)
            .then(() => {
                getUserProducts()
            })
    }

    return (
        <Card key={product?.id} style={{
            width: '18rem'
        }}
        >
            <button className="btn" onClick={(() => deleteButton())} ><i className="fa-solid fa-xmark"></i></button>
            <Col>
                <CardBody onClick={() => navigate(`/product/${product?.id}`)}>
                    <CardImg top width="100%" src={product?.imageUrl} alt="Card image cap" />

                    <p>{product?.brand?.name}</p>

                    <p>{product?.name}</p>


                </CardBody>
            </Col>
        </Card >
    )
}