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
        <section className="productdetails">

            <div className="productdetailsinfo">
                <p>
                    {
                        isAdmin
                            ? <>
                                <button className="btn" onClick={(() => navigate(`/product/delete/${product.id}`))}><i className="fa-solid fa-trash-can"></i></button>
                                <button className="btn" onClick={(() => navigate(`/product/edit/${product.id}`))}><i className="fa-solid fa-eye-dropper"></i></button></>
                            : ""

                    }
                </p>


                <p className="Name panel-item">
                    <span className="title"> {product?.name} <button onClick={(() => addUserProduct(product))} className="btn"><i className="fa-solid fa-person-circle-plus"></i></button></span>
                    <span className="title">{product?.brand?.name} </span>
                </p>
                <p> {product?.type?.name}</p>
                <p>${product?.price?.toFixed(2)}</p>


            </div>

            <div className="productdetailsimg">
                <img alt="Card image cap" className="productDetailImage card-img" src={product?.imageUrl} />
            </div>
        </section>
        <section className="productingredientsection">
            <p className="product-title">Product Ingredients {
                isApproved
                    ? <button className="btn" onClick={(() => setFormActive(!formActive))}><i className="fa-solid fa-plus"></i></button>
                    : ""
            }</p>
            <section className="piformsection">
                {
                    formActive
                        ? <div className="piform"><ProductIngredientForm getPIs={getPIs} product={product} setFormActive={setFormActive} getProduct={getProduct} /></div>
                        : ""

                }
            </section>
            <div className="productIngredientList">

                <Accordion open={open} toggle={toggle}>
                    {
                        productIngredients.map((pi) => (
                            <div key={pi.id} className="productIngredient"><ProductIngredient getPIs={getPIs} productIngredient={pi} getProduct={getProduct} key={pi.id} isAdmin={isAdmin} isApproved={isApproved} /></div>
                        ))
                    }
                </Accordion>
            </div>


        </section>




    </>
}