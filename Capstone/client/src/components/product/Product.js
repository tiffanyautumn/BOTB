import React from "react";
import { NavLink, useNavigate } from "react-router-dom";
import { Button, Card, CardBody, CardImg, Col } from "reactstrap";
import { NavLink as RRNavLink } from "react-router-dom";

export const Product = ({ product }) => {
    const navigate = useNavigate()
    return (
        <Card key={product?.id} style={{
            width: '18rem'
        }}
        >
            <Col>
                <CardBody>
                    <CardImg top width="100%" src={product?.imageUrl} alt="Card image cap" />

                    <p>{product?.brand}</p>
                    <Button onClick={() => navigate(`/product/${product?.id}`)}>{product?.name}</Button>
                    <p>${product.price}</p>

                </CardBody>
            </Col>
        </Card >
    )
}