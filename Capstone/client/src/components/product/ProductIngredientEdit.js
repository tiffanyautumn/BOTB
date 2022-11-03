import { useEffect, useState } from "react"
import { Button, Card, CardBody, FormGroup, Input, Label, Form, CardText } from "reactstrap"
import { editProductIngredient, getProductIngredientById } from "../../modules/productIngredientManager"

export const ProductIngredientEdit = ({ ProductIngredient, setFormActive, getProduct }) => {

    const [productIngredient, updateProductIngredient] = useState({
        activeIngredient: false,
        order: 0,
    })


    const getProductIngredient = () => {
        getProductIngredientById(ProductIngredient.id).then(i => updateProductIngredient(i))
    }

    useEffect(
        () => {
            getProductIngredient()
        }, []
    )

    const handleSaveButtonClick = (event) => {
        event.preventDefault()


        const productIngredientToSend = {
            ingredientId: productIngredient.ingredientId,
            productId: productIngredient.productId,
            activeIngredient: productIngredient.active,
            order: parseInt(productIngredient.order),
            id: productIngredient.id
        }

        return editProductIngredient(productIngredient.id, productIngredientToSend)
            .then(() => {
                setFormActive(false)
                getProduct()
            })
    }
    return (<>
        <Card>
            <CardBody>
                <CardText>{productIngredient?.ingredient?.name}</CardText>
                <Form className="ingredientForm">
                    <FormGroup>
                        <Label for="activeIngredient">Active</Label>
                        <Input
                            id="activeIngredient"
                            name="activeIngredient"
                            type="checkbox"
                            value={productIngredient.activeIngredient}
                            onChange={
                                (evt) => {
                                    const copy = { ...productIngredient }
                                    copy.activeIngredient = evt.target.checked
                                    updateProductIngredient(copy)
                                }
                            } />
                    </FormGroup>

                    <FormGroup>
                        <Label for="order">Order</Label>
                        <Input
                            id="order"
                            name="order"
                            type="number"
                            value={productIngredient.order}
                            onChange={
                                (evt) => {
                                    const copy = { ...productIngredient }
                                    copy.order = evt.target.value
                                    updateProductIngredient(copy)
                                }
                            } />
                    </FormGroup>


                    <Button onClick={(clickEvent) => handleSaveButtonClick(clickEvent)}
                        className="btn btn-primary">
                        Save
                    </Button>
                </Form>
                <Button onClick={() => setFormActive(false)}>Cancel</Button>
            </CardBody>
        </Card>

    </>)
}