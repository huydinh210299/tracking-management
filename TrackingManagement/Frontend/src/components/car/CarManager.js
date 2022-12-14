import {
  Table,
  Input,
  Button,
  Select,
  Drawer,
  Form,
  notification,
  Tooltip,
  Space,
  Card,
} from "antd"
import {
  CheckOutlined,
  RedoOutlined,
  CloseCircleOutlined,
} from "@ant-design/icons"
import { useEffect, useState } from "react"

import makeRequest from "../../utils/makeRequest"
import { RECORD_MODE } from "../../const/mode"
import { addListKey } from "../../utils/addListKey"
import omitNil from "../../utils/omit"
import { carConfig } from "../../config/car"
import rfidHandle from "../../handle/rfid"
import { requestUrl } from "../../resource/requestUrl"
import SearchSelect from "../seachItem/SearchSelect"
import SearchText from "../seachItem/SearchText"
import CustomSkeleton from "../skeleton/CustomSkeleton"

const { Option } = Select

const CarManager = () => {
  const [cars, setCars] = useState([])
  const [userUnits, setUserUnits] = useState([])
  const [drivers, setDrivers] = useState([])
  const [rfids, setRfids] = useState([])
  const [displayRfids, setDisplayRfids] = useState([])
  const [pagination, setPagination] = useState({
    current: 1,
    pageSize: 10,
    total: 0,
    showTotal: (total, range) => `${range[0]}-${range[1]} of ${total}`,
  })
  const [filter, setFilter] = useState({
    licensePlate: "",
    type: "",
    unitId: null,
    driverId: null,
  })
  const [lastFilter, setLastFilter] = useState({
    licensePlate: "",
    type: "",
    unitId: null,
    driverId: null,
  })
  const [selectedRowArr, setSelectedRowArr] = useState([])
  const [visible, setVisible] = useState(false)
  const [mode, setMode] = useState(RECORD_MODE.CREATE)
  const [loading, setLoading] = useState(true)
  const [selectedRecord, setSelectedRecord] = useState({})

  const [form] = Form.useForm()

  const handleSearch = async (params, pagination = {}) => {
    setLoading(true)
    let filter = omitNil(params)
    makeRequest({
      method: "GET",
      url: requestUrl.car.readUrl(),
      params: {
        ...filter,
        page: pagination.current,
        record: pagination.pageSize,
      },
    }).then((searchRs) => {
      setCars(addListKey(searchRs.data))
      setPagination({
        ...pagination,
        total: searchRs.totalRecords,
      })
      setLoading(false)
    })
  }

  const handlePaginationChange = (page, pageSize) => {
    let newPagination = { ...pagination, current: page, pageSize }
    handleSearch(filter, newPagination)
  }

  const columns = [
    {
      title: "Bi???n xe",
      dataIndex: "licensePlate",
      width: 200,
    },
    {
      title: "Lo???i xe",
      dataIndex: "type",
      width: 350,
    },
    {
      title: "????n v???",
      render: (text, record) => {
        return <>{record.unit ? record.unit.name : ""}</>
      },
    },
    {
      title: "L??i xe ch??nh",
      render: (text, record) => {
        return <>{record.driver ? record.driver.name : ""}</>
      },
      width: 200,
    },
    {
      title: "Th??? RFID",
      dataIndex: "rfidId",
      render: (text, record) => {
        return <>{record.rfid ? record.rfid.cardNumber : ""}</>
      },
      width: 350,
    },
    {
      title: "Xo??",
      render: (text, record) => (
        <CloseCircleOutlined style={{ color: "red" }} />
      ),
      align: "center",
      width: 50,
    },
  ]

  // Function handle selectedRow
  const onRowSelected = (record, selected, selectedRows) => {
    setVisible(true)
    let selectedRecord = selectedRows[0]
    setSelectedRecord(selectedRecord)
    form.resetFields()
    form.setFieldsValue(selectedRecord)
    setMode(RECORD_MODE.UPDATE)

    setDisplayRfids(
      rfidHandle.setListRfid(rfids, selectedRows[0].rfid, RECORD_MODE.UPDATE)
    )
  }

  const onSelectedChange = (selectedRowKeys, selectedRows) => {
    setSelectedRowArr(selectedRowKeys)
  }

  // Function handle drawer
  const onClose = () => {
    setSelectedRowArr([])
    setSelectedRecord({})
    setVisible(false)
  }

  const handleReloadRfid = () => {
    makeRequest({
      method: "GET",
      url: requestUrl.rfid.readUrl(),
      params: {
        type: 1,
        isDistributed: false,
        paging: false,
      },
    })
      .then((rs) => {
        let rfids = rs.data
        setRfids(rfids)
        setDisplayRfids(
          rfidHandle.setListRfid(rfids, selectedRecord.rfid, mode)
        )
      })
      .catch((err) => {
        console.log(err)
      })
  }

  useEffect(() => {
    const getData = async () => {
      let [carRs, userUnitRs, rfidRs, driverRs] = await Promise.all([
        makeRequest({
          method: "GET",
          url: requestUrl.car.readUrl(),
        }),
        makeRequest({
          method: "GET",
          url: requestUrl.userUnit.readUrl(),
        }),
        makeRequest({
          method: "GET",
          url: requestUrl.rfid.readUrl(),
          params: {
            type: 1,
            isDistributed: false,
            paging: false,
          },
        }),
        makeRequest({
          method: "GET",
          url: requestUrl.driver.readUrl(),
          params: {
            paging: false,
          },
        }),
      ])

      setCars(addListKey(carRs.data))
      setDrivers(addListKey(driverRs.data))
      setRfids(addListKey(rfidRs.data))
      setDisplayRfids(
        rfidHandle.setListRfid(rfidRs.data, selectedRecord.rfid, mode)
      )
      setUserUnits(addListKey(userUnitRs.data))
      setPagination({ ...pagination, total: carRs.totalRecords })
      setLoading(false)
    }

    getData()
  }, [])

  const onFinish = (car) => {
    setLoading(true)
    let method = mode === RECORD_MODE.CREATE ? "POST" : "PUT"
    let url =
      mode === RECORD_MODE.CREATE
        ? requestUrl.car.createUrl()
        : requestUrl.car.updateUrl({ id: car.id })
    let msg =
      mode === RECORD_MODE.CREATE
        ? "Th??m xe m???i th??nh c??ng"
        : "C???p nh???t th??nh c??ng"
    makeRequest({
      method,
      url,
      data: car,
    }).then((rs) => {
      notification.open({
        message: "Th??ng b??o",
        icon: <CheckOutlined style={{ color: "#2fd351" }} />,
        description: msg,
      })
      handleSearch(filter, pagination)
      handleReloadRfid(mode)
      setSelectedRowArr([])
      setSelectedRecord({})
      setVisible(false)
      setLoading(false)
    })
  }

  const openCreateForm = () => {
    setVisible(true)
    setMode(RECORD_MODE.CREATE)
    setSelectedRecord({})
    form.resetFields()
    form.setFieldsValue({})

    setDisplayRfids(
      rfidHandle.setListRfid(rfids, selectedRecord.rfid, RECORD_MODE.CREATE)
    )
  }

  return (
    <>
      <Card
        type="middle"
        title="Danh s??ch xe"
        bordered={true}
        style={{ height: "100%" }}
        extra={
          <Button type="primary" onClick={openCreateForm}>
            Th??m xe m???i
          </Button>
        }
        bodyStyle={{ padding: 10 }}
      >
        <Space style={{ marginBottom: 10 }}>
          <SearchSelect
            dataIndex="unitId"
            filter={filter}
            pagination={pagination}
            handleSearch={handleSearch}
            placeholder={"Ch???n ????n v???"}
            setFilter={setFilter}
            setLastFilter={setLastFilter}
            data={userUnits.map((item) => ({
              value: item.id,
              label: item.name,
            }))}
          ></SearchSelect>
          <SearchText
            dataIndex="licensePlate"
            filter={filter}
            pagination={pagination}
            handleSearch={handleSearch}
            placeholder={"Nh???p bi???n s??? xe"}
            setFilter={setFilter}
            setLastFilter={setLastFilter}
            lastFilter={lastFilter}
          ></SearchText>
        </Space>
        <Table
          dataSource={cars}
          columns={columns}
          pagination={{
            position: ["bottomRight"],
            ...pagination,
            onChange: handlePaginationChange,
          }}
          bordered
          size="small"
          rowSelection={{
            selectedRowKeys: selectedRowArr,
            columnTitle: <div>S???a</div>,
            type: "checkbox",
            onSelect: (record, selected, selectedRows) =>
              onRowSelected(record, selected, selectedRows),
            onChange: (selectedRowKeys, selectedRows) =>
              onSelectedChange(selectedRowKeys, selectedRows),
          }}
        />
      </Card>
      <Drawer
        title={mode === RECORD_MODE.CREATE ? "Th??m xe m???i" : "C???p nh???t"}
        placement="right"
        onClose={onClose}
        visible={visible}
        width="450"
      >
        <Form
          name="carForm"
          labelCol={{ span: 8 }}
          wrapperCol={{ span: 16 }}
          initialValues={selectedRecord}
          labelAlign="left"
          form={form}
          onFinish={onFinish}
        >
          <Form.Item label="id" name="id" noStyle>
            <Input type="hidden" />
          </Form.Item>
          <Form.Item
            label="Bi???n s???"
            name="licensePlate"
            rules={[
              {
                required: true,
                message: "Tr?????ng n??y kh??ng ???????c thi???u",
              },
            ]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            label="Lo???i xe"
            name="type"
            rules={[
              {
                required: true,
                message: "Tr?????ng n??y kh??ng ???????c thi???u",
              },
            ]}
          >
            <Input />
          </Form.Item>

          <Form.Item
            name="rfidId"
            label={
              <>
                M?? thi???t b??? [
                <Tooltip title="T???i l???i m?? thi???t b???">
                  <RedoOutlined
                    onClick={() => handleReloadRfid(mode)}
                    style={{ color: "#1890ff" }}
                  />
                </Tooltip>
                ]
              </>
            }
          >
            <Select>
              {displayRfids.map((item, index) => (
                <Option value={item.id} key={index}>
                  {item.cardNumber}
                </Option>
              ))}
            </Select>
          </Form.Item>

          <Form.Item
            name="unitId"
            label="????n v??? qu???n l??"
            rules={[
              {
                required: true,
                message: "Tr?????ng n??y kh??ng ???????c thi???u",
              },
            ]}
          >
            <Select>
              {userUnits.map((item, index) => (
                <Option value={item.id} key={index}>
                  {item.name}
                </Option>
              ))}
            </Select>
          </Form.Item>

          <Form.Item name="driverId" label="L??i xe">
            <Select>
              {drivers.map((item, index) => (
                <Option value={item.id} key={index}>
                  {item.name}
                </Option>
              ))}
            </Select>
          </Form.Item>

          <Form.Item
            name="numberCamera"
            label="S??? l?????ng camera"
            rules={[
              {
                required: true,
                message: "Tr?????ng n??y kh??ng ???????c thi???u",
              },
            ]}
          >
            <Select>
              <Option value="1" key="1">
                1
              </Option>
              <Option value="2" key="2">
                2
              </Option>
            </Select>
          </Form.Item>

          <Form.Item name="firstCamPo" label="Camera 1">
            <Select>
              {carConfig.firstCamPosition.map((item, index) => (
                <Option value={item.value} key={index}>
                  {item.label}
                </Option>
              ))}
            </Select>
          </Form.Item>

          <Form.Item name="firstCamRotation" label="G??c quay cam 1">
            <Select>
              {carConfig.cameraRotation.map((item, index) => (
                <Option value={item.value} key={index}>
                  {item.label}
                </Option>
              ))}
            </Select>
          </Form.Item>

          <Form.Item name="secondCamRotation" label="G??c quay cam 2">
            <Select>
              {carConfig.cameraRotation.map((item, index) => (
                <Option value={item.value} key={index}>
                  {item.label}
                </Option>
              ))}
            </Select>
          </Form.Item>

          <Form.Item name="fuel" label="Nhi??n li???u">
            <Select>
              {carConfig.fuel.map((item, index) => (
                <Option value={item.value} key={index}>
                  {item.label}
                </Option>
              ))}
            </Select>
          </Form.Item>

          <Form.Item label="Gi???i h???n t???c ?????" name="limitedSpeed">
            <Input />
          </Form.Item>

          <Form.Item
            wrapperCol={{
              offset: 0,
              span: 24,
            }}
          >
            <Button type="primary" htmlType="submit">
              L??u
            </Button>
          </Form.Item>
        </Form>
      </Drawer>
      <CustomSkeleton loading={loading} />
    </>
  )
}

export default CarManager
