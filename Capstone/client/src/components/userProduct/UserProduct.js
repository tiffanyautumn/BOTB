import React from "react";
import { NavLink, useNavigate } from "react-router-dom";
import { Button, Card, CardBody, CardImg, CardText, Col } from "reactstrap";
import { NavLink as RRNavLink } from "react-router-dom";
import { addUserProduct, deleteUserProduct } from "../../modules/productManager";

export const UserProduct = ({ product, userProduct, userProductId, getAllUserProducts }) => {
    const navigate = useNavigate()

    const deleteButton = () => {
        return deleteUserProduct(userProductId)
            .then(() => {
                getAllUserProducts()
            })
    }

    return (
        <Card key={product?.id} style={{
            width: '18rem'
        }}
        >
            <Col>
                <CardBody>
                    <CardImg top width="100%" src={product?.imageUrl} alt="Card image cap" />

                    <p>{product?.brand?.name}</p>

                    <Button onClick={() => navigate(`/product/${product?.id}`)}>{product?.name}</Button>
                    {
                        userProduct ? <button className="btn" onClick={(() => deleteButton())} ><i className="fa-solid fa-xmark"></i></button> : <button onClick={(() => addUserProduct(product))} className="btn"><i class="fa-solid fa-person-circle-plus"></i></button>
                    }

                </CardBody>
            </Col>
        </Card >
    )
}