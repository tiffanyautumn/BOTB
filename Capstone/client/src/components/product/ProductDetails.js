import { useEffect, useState } from "react"
import { useNavigate, useParams } from "react-router-dom"
import { Accordion, Button, Card, CardBody, CardImg, CardText, Col, Row, Table } from "reactstrap"
import { addUserProduct, getProductById } from "../../modules/productManager"
import { ProductIngredient } from "./ProductIngredient"
import { ProductIngredientForm } from "./ProductIngredientForm"
import './Product.css';
import { getPIByProductId } from "../../modules/productIngredientManager"

export const ProductDetails = ({ isAdmin, isApproved }) => {
    const [open, setOpen] = useState('1');
    const { productId } = useParams()
    const navigate = useNavigate()
    const [product, setProduct] = useState([])
    const [productIngredients, setProductIngredients] = useState([])
    const [formActive, setFormActive] = useState(false)
    const toggle = (id) => {
        if (open === id) {
            setOpen();
        } else {
            setOpen(id);
        }
    };

    const getProduct = () => {
        getProductById(productId).then(p => setProduct(p))
    }

    const getPIs = () => {
        getPIByProductId(productId).then(pi => setProductIngredients(pi))
    }
    useEffect(
        () => {
            getProduct()
            getPIs()
        }, []
    )


    return <>

        <Card key={product.id} style={{
            width: '75%'
        }}
        >

            <CardBody>

                <Card style={{
                    width: '15%'
                }}>
                    <CardImg className="productDetailImage" height="15%" width="25%" src={product?.imageUrl} alt="Card image cap" />
                </Card>
                <button className="btn"><i class="fa-solid fa-comment-dollar"></i></button>
                <CardText>
                    <p>{product?.brand?.name}</p>
                    <p>{product?.name} <button onClick={(() => addUserProduct(product))} className="btn"><i class="fa-solid fa-person-circle-plus"></i></button></p>
                    <p>{product?.type?.name}</p>
                    ${product?.price?.toFixed(2)}
                </CardText>



                <div>
                    <Accordion open={open} toggle={toggle}>
                        {
                            productIngredients.map((pi) => (
                                <ProductIngredient getPIs={getPIs} productIngredient={pi} getProduct={getProduct} key={pi.id} isAdmin={isAdmin} isApproved={isApproved} />
                            ))
                        }
                    </Accordion>
                </div>

                {
                    formActive
                        ? <ProductIngredientForm getPIs={getPIs} product={product} setFormActive={setFormActive} getProduct={getProduct} />
                        : ""

                }
                {
                    isApproved
                        ? <p><Button onClick={(() => setFormActive(true))}>Add a new ingredient</Button></p>
                        : ""
                }
                {
                    isAdmin
                        ? <>
                            <Button onClick={(() => navigate(`/product/delete/${product.id}`))}>delete</Button>
                            <Button onClick={(() => navigate(`/product/edit/${product.id}`))}>edit</Button></>
                        : ""

                }

            </CardBody>

        </Card >
    </>
}