import { useEffect, useState } from "react"
import { useNavigate, useParams } from "react-router-dom"
import { Button, Card, CardBody, CardImg, CardText, Col, Row, Table } from "reactstrap"
import { getProductById } from "../../modules/productManager"
import { ProductIngredient } from "./ProductIngredient"
import { ProductIngredientForm } from "./ProductIngredientForm"
import './Product.css';

export const ProductDetails = ({ isAdmin, isApproved }) => {
    const { productId } = useParams()
    const navigate = useNavigate()
    const [product, setProduct] = useState([])
    const [formActive, setFormActive] = useState(false)

    const getProduct = () => {
        getProductById(productId).then(p => setProduct(p))
    }

    useEffect(
        () => {
            getProduct()
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

                <CardText>
                    <p>{product?.brand?.name}</p>
                    <p>{product?.name}</p>
                    <p>{product?.type?.name}</p>
                    ${product?.price?.toFixed(2)}
                </CardText>


                <Table>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Use</th>
                            <th>Safety</th>
                            <th>Rating</th>
                            <th></th>
                            <th></th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            product?.productIngredients?.map((pi) => (
                                <ProductIngredient productIngredient={pi} getProduct={getProduct} key={pi.id} isAdmin={isAdmin} isApproved={isApproved} />
                            ))
                        }
                    </tbody>

                </Table>
                {
                    formActive
                        ? <ProductIngredientForm product={product} setFormActive={setFormActive} getProduct={getProduct} />
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