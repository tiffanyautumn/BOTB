import { useEffect, useState } from "react"
import { useNavigate, useParams } from "react-router-dom"
import { Button, Card, CardBody, CardImg, Col, Table } from "reactstrap"
import { getProductById } from "../../modules/productManager"
import { ProductIngredient } from "./ProductIngredient"

export const ProductDetails = () => {
    const { productId } = useParams()
    const navigate = useNavigate()
    const [product, setProduct] = useState([])

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
            <Col>
                <CardBody>
                    <CardImg top width="100%" src={product?.imageUrl} alt="Card image cap" />

                    <p>{product?.brand}</p>
                    <p>{product?.name}</p>
                    <Table>
                        <thead>
                            <tr>
                                <th>name</th>
                                <th>use</th>
                                <th>safety</th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                product?.productIngredients?.map((pi) => (
                                    <ProductIngredient productIngredient={pi} key={pi.id} />
                                ))
                            }
                        </tbody>
                    </Table>
                    <Button onClick={(() => navigate(`/product/delete/${product.id}`))}>delete</Button>

                </CardBody>
            </Col>
        </Card >
    </>
}